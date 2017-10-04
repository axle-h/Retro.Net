using System;
using GameBoy.Net.Devices;
using GameBoy.Net.Registers.Interfaces;

namespace GameBoy.Net.Registers
{
    /// <summary>
    /// Serial port that will transfer bytes synchronously.
    /// Warning! This will lock up the CPU if the Transfer function on the other SerialPort do any work.
    /// </summary>
    /// <seealso cref="SerialPortBase" />
    public class SyncSerialPort : SerialPortBase
    {
        private const byte TransferStartFlagMask = 0x80;
        private const byte IsFastModeMask = 0x02;
        private const byte IsInternalClockMask = 0x01;

        private static readonly TimeSpan ExternalTransferTimeout = TimeSpan.FromSeconds(1);
        // Real GB doesn't have timeout but we can't lock this thread forever.

        private readonly IInterruptFlagsRegister _interruptFlagsRegister;

        private bool _isFastMode;
        private bool _isInternalClock;

        // private TaskCompletionSource<bool> _transferredTaskSource;
        private bool _transferStartFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncSerialPort"/> class.
        /// </summary>
        /// <param name="interruptFlagsRegister">The interrupt flags register.</param>
        public SyncSerialPort(IInterruptFlagsRegister interruptFlagsRegister)
        {
            _interruptFlagsRegister = interruptFlagsRegister;
        }

        /// <summary>
        /// Bit 7 - Transfer Start Flag
        /// 0: Non transfer
        /// 1: Start transfer
        /// Bit 0 - Shift Clock
        /// 0: External Clock(500KHz Max.)
        /// 1: Internal Clock(8192Hz)
        /// Transfer is initiated by setting the
        /// Transfer Start Flag.This bit may be read
        /// and is automatically set to 0 at the end
        /// of Transfer.
        /// </summary>
        public override byte Register
        {
            get
            {
                var value = 0x00;
                if (_transferStartFlag)
                {
                    value |= TransferStartFlagMask;
                }

                if (_isFastMode)
                {
                    value |= IsFastModeMask;
                }

                if (_isInternalClock)
                {
                    value |= IsInternalClockMask;
                }

                return (byte) value;
            }
            set
            {
                _transferStartFlag = (value & TransferStartFlagMask) > 0;
                if (!_transferStartFlag)
                {
                    // No transfer, nothing to transfer to.
                    Reset();
                    return;
                }

                _isFastMode = (value & IsFastModeMask) > 0;
                _isInternalClock = (value & IsInternalClockMask) > 0;

                if (_isInternalClock)
                {
                    SerialData.Register = ConnectedSerialPort?.Transfer(SerialData.Register) ?? 0x00;
                }
                else
                {
                    // Wait... Except we also need to be listening for interrupts some how...
                    // _transferredTaskSource = new TaskCompletionSource<bool>();
                    // _transferredTaskSource.Task.Wait(ExternalTransferTimeout);

                    // Not implemented.
                    SerialData.Register = 0x00;
                }

                Reset();
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            _transferStartFlag = false;
            _isFastMode = false;
            _isInternalClock = false;
        }

        /// <summary>
        /// Transfer a byte to external serial port and read a byte from external serial port.
        /// Only the serial port running the GB internal clock should be calling this method.
        /// </summary>
        /// <param name="value">The value to write to this port.</param>
        /// <returns>The value to transfer to the connected port.</returns>
        public override byte Transfer(byte value)
        {
            var tmp = SerialData.Register;
            SerialData.Register = value;

            //_transferredTaskSource?.TrySetResult(true);

            _interruptFlagsRegister.UpdateInterrupts(InterruptFlag.SerialLink);
            return tmp;
        }
    }
}