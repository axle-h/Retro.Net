export class SocialLink {

  /**
   * Constracts a new social link.
   * @param {string} name FontAwesome compatible displayName.
   * @param {string} url social provider URL.
   */
  constructor(name: string, url: string) {
    this.name = name;
    this.url = url;
  }

  /**
   * The FontAwesome compatible displayName.
   */
  public name: string;

  /**
   * The social provider URL.
   */
  public url: string;
}
