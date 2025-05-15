export class City {
  cityId: string | null;
  cityName: string | null;

  constructor(cityId: string | null, cityName: string | null) {
    this.cityId = cityId;
    this.cityName = cityName;
  }
}
