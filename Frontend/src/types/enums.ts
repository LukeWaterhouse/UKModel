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

export const DnoRegion = {
  ScottishHydroElectric: 0,
  SpDistribution: 1,
  ElectricityNorthWest: 2,
  NpgNorthEast: 3,
  NpgYorkshire: 4,
  SpManweb: 5,
  WpdSouthWales: 6,
  WpdWestMidlands: 7,
  WpdEastMidlands: 8,
  UkpnEast: 9,
  WpdSouthWest: 10,
  SseSouth: 11,
  UkpnLondon: 12,
  UkpnSouthEast: 13,
  England: 14,
  Scotland: 15,
  Wales: 16,
  Gb: 17,
} as const;
export type DnoRegion = (typeof DnoRegion)[keyof typeof DnoRegion];

export const PlantSource = {
  Nuclear: 0,
  Wind: 1,
  Solar: 2,
  Gas: 3,
  Coal: 4,
  Hydro: 5,
  Oil: 6,
  Biomass: 7,
  Waste: 8,
  Biogas: 9,
} as const;
export type PlantSource = (typeof PlantSource)[keyof typeof PlantSource];
