import type { CarbonIntensityIndex, FuelType, GridRegionType } from './enums';

export interface GenerationMixEntry {
  fuelType: FuelType;
  percentage: number;
}

export interface IntensityData {
  forecast: number;
  actual: number | null;
  index: CarbonIntensityIndex;
  unit: string;
}

export interface RegionIntensity {
  regionType: GridRegionType;
  dnoRegion: string;
  intensity: IntensityData;
  generationMix: GenerationMixEntry[];
}

export interface RegionalIntensityResponse {
  retrievedAt: string;
  regions: RegionIntensity[];
}

export interface NationalGenerationMixResponse {
  from: string;
  to: string;
  mix: GenerationMixEntry[];
}
