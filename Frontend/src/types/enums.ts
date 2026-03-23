export const GridRegionType = {
  NorthScotland: 1,
  SouthScotland: 2,
  NorthWestEngland: 3,
  NorthEastEngland: 4,
  Yorkshire: 5,
  NorthWales: 6,
  SouthWales: 7,
  WestMidlands: 8,
  EastMidlands: 9,
  EastEngland: 10,
  SouthWestEngland: 11,
  SouthEngland: 12,
  London: 13,
  SouthEastEngland: 14,
  England: 15,
  Scotland: 16,
  Wales: 17,
} as const;
export type GridRegionType = (typeof GridRegionType)[keyof typeof GridRegionType];

export const FuelType = {
  Gas: 0,
  Coal: 1,
  Biomass: 2,
  Nuclear: 3,
  Hydro: 4,
  Imports: 5,
  Wind: 6,
  Solar: 7,
  Storage: 8,
  Other: 9,
} as const;
export type FuelType = (typeof FuelType)[keyof typeof FuelType];

export const CarbonIntensityIndex = {
  VeryLow: 0,
  Low: 1,
  Moderate: 2,
  High: 3,
  VeryHigh: 4,
} as const;
export type CarbonIntensityIndex = (typeof CarbonIntensityIndex)[keyof typeof CarbonIntensityIndex];
