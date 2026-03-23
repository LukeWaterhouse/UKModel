import type { NationalGenerationMixResponse } from '../types/energy';
import { FuelType, CarbonIntensityIndex } from '../types/enums';

const FUEL_TYPE_LABEL = Object.fromEntries(
  Object.entries(FuelType).map(([name, id]) => [id, name]),
) as Record<number, string>;

const INTENSITY_INDEX_LABEL: Record<number, string> = {
  [CarbonIntensityIndex.VeryLow]: 'Very Low',
  [CarbonIntensityIndex.Low]: 'Low',
  [CarbonIntensityIndex.Moderate]: 'Moderate',
  [CarbonIntensityIndex.High]: 'High',
  [CarbonIntensityIndex.VeryHigh]: 'Very High',
};

const INTENSITY_INDEX_COLOR: Record<number, string> = {
  [CarbonIntensityIndex.VeryLow]: '#00c800',
  [CarbonIntensityIndex.Low]: '#64dc32',
  [CarbonIntensityIndex.Moderate]: '#ffc800',
  [CarbonIntensityIndex.High]: '#ff6400',
  [CarbonIntensityIndex.VeryHigh]: '#ff1e1e',
};

interface NationalSummaryProps {
  data: NationalGenerationMixResponse | null;
}

export default function NationalSummary({ data }: NationalSummaryProps) {
  if (!data) return null;

  const sorted = [...data.mix].sort((a, b) => b.percentage - a.percentage);

  return (
    <div style={containerStyle}>
      <div style={headerStyle}>
        <h2 style={titleStyle}>National Overview</h2>
        <div style={intensityContainerStyle}>
          <span style={intensityValueStyle}>
            {data.intensity.forecast} {data.intensity.unit}
          </span>
          <span
            style={{
              ...badgeStyle,
              backgroundColor: INTENSITY_INDEX_COLOR[data.intensity.index] ?? '#888',
            }}
          >
            {INTENSITY_INDEX_LABEL[data.intensity.index] ?? 'Unknown'}
          </span>
        </div>
      </div>
      <div style={mixRowStyle}>
        {sorted
          .filter((e) => e.percentage > 0)
          .map((entry) => (
            <div key={entry.fuelType} style={mixItemStyle}>
              <span style={mixPercentStyle}>{entry.percentage.toFixed(1)}%</span>
              <span style={mixLabelStyle}>{FUEL_TYPE_LABEL[entry.fuelType] ?? 'Other'}</span>
            </div>
          ))}
      </div>
    </div>
  );
}

const containerStyle: React.CSSProperties = {
  backgroundColor: '#1a1a2e',
  color: '#e0e0e0',
  padding: '12px 16px',
  borderRadius: '8px 8px 0 0',
  borderBottom: '1px solid #333',
};

const headerStyle: React.CSSProperties = {
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
  marginBottom: 8,
};

const titleStyle: React.CSSProperties = {
  margin: 0,
  fontSize: 16,
  fontWeight: 600,
  color: '#fff',
};

const intensityContainerStyle: React.CSSProperties = {
  display: 'flex',
  alignItems: 'center',
  gap: 8,
};

const intensityValueStyle: React.CSSProperties = {
  fontSize: 14,
  fontWeight: 500,
};

const badgeStyle: React.CSSProperties = {
  fontSize: 11,
  fontWeight: 600,
  padding: '2px 6px',
  borderRadius: 4,
  color: '#000',
};

const mixRowStyle: React.CSSProperties = {
  display: 'flex',
  flexWrap: 'wrap',
  gap: '8px 16px',
};

const mixItemStyle: React.CSSProperties = {
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
  minWidth: 50,
};

const mixPercentStyle: React.CSSProperties = {
  fontSize: 14,
  fontWeight: 600,
  fontVariantNumeric: 'tabular-nums',
};

const mixLabelStyle: React.CSSProperties = {
  fontSize: 11,
  color: '#999',
};
