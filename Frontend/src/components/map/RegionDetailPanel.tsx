import type { RegionIntensity } from '../../types/energy';
import { FuelType, CarbonIntensityIndex, DnoRegion } from '../../types/enums';

const FUEL_TYPE_LABEL = Object.fromEntries(
  Object.entries(FuelType).map(([name, id]) => [id, name]),
) as Record<number, string>;

const DNO_REGION_LABEL: Record<number, string> = {
  [DnoRegion.ScottishHydroElectric]: 'Scottish Hydro Electric Power Distribution',
  [DnoRegion.SpDistribution]: 'SP Distribution',
  [DnoRegion.ElectricityNorthWest]: 'Electricity North West',
  [DnoRegion.NpgNorthEast]: 'NPG North East',
  [DnoRegion.NpgYorkshire]: 'NPG Yorkshire',
  [DnoRegion.SpManweb]: 'SP Manweb',
  [DnoRegion.WpdSouthWales]: 'WPD South Wales',
  [DnoRegion.WpdWestMidlands]: 'WPD West Midlands',
  [DnoRegion.WpdEastMidlands]: 'WPD East Midlands',
  [DnoRegion.UkpnEast]: 'UKPN East',
  [DnoRegion.WpdSouthWest]: 'WPD South West',
  [DnoRegion.SseSouth]: 'SSE South',
  [DnoRegion.UkpnLondon]: 'UKPN London',
  [DnoRegion.UkpnSouthEast]: 'UKPN South East',
  [DnoRegion.England]: 'England',
  [DnoRegion.Scotland]: 'Scotland',
  [DnoRegion.Wales]: 'Wales',
  [DnoRegion.Gb]: 'GB',
};

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

interface RegionDetailPanelProps {
  region: RegionIntensity | null;
  regionName: string | null;
}

export default function RegionDetailPanel({ region, regionName }: RegionDetailPanelProps) {
  return (
    <div style={panelStyle}>
      {!region ? (
        <p style={placeholderStyle}>Hover over a region to see details</p>
      ) : (
        <>
          <h3 style={titleStyle}>{regionName ?? 'Unknown Region'}</h3>
          <p style={subtitleStyle}>{DNO_REGION_LABEL[region.dnoRegion] ?? 'Unknown DNO'}</p>

          <div style={sectionStyle}>
            <h4 style={sectionTitleStyle}>Carbon Intensity</h4>
            <div style={intensityRowStyle}>
              <span style={intensityValueStyle}>
                {region.intensity.forecast} {region.intensity.unit}
              </span>
              <span
                style={{
                  ...intensityBadgeStyle,
                  backgroundColor: INTENSITY_INDEX_COLOR[region.intensity.index] ?? '#888',
                }}
              >
                {INTENSITY_INDEX_LABEL[region.intensity.index] ?? 'Unknown'}
              </span>
            </div>
          </div>

          <div style={sectionStyle}>
            <h4 style={sectionTitleStyle}>Generation Mix</h4>
            <ul style={listStyle}>
              {[...region.generationMix]
                .sort((a, b) => b.percentage - a.percentage)
                .map((entry) => (
                  <li key={entry.fuelType} style={listItemStyle}>
                    <span style={fuelLabelStyle}>
                      {FUEL_TYPE_LABEL[entry.fuelType] ?? 'Other'}
                    </span>
                    <div style={barContainerStyle}>
                      <div
                        style={{
                          ...barFillStyle,
                          width: `${Math.max(entry.percentage, 0.5)}%`,
                        }}
                      />
                    </div>
                    <span style={percentStyle}>{entry.percentage.toFixed(1)}%</span>
                  </li>
                ))}
            </ul>
          </div>
        </>
      )}
    </div>
  );
}

const panelStyle: React.CSSProperties = {
  width: 320,
  flexShrink: 0,
  backgroundColor: '#1a1a2e',
  color: '#e0e0e0',
  padding: '20px 16px',
  overflowY: 'auto',
  borderLeft: '1px solid #333',
  display: 'flex',
  flexDirection: 'column',
};

const placeholderStyle: React.CSSProperties = {
  color: '#888',
  margin: 'auto 0',
  textAlign: 'center',
  fontSize: 14,
};

const titleStyle: React.CSSProperties = {
  margin: '0 0 2px',
  fontSize: 18,
  fontWeight: 600,
  color: '#fff',
};

const subtitleStyle: React.CSSProperties = {
  margin: '0 0 16px',
  fontSize: 13,
  color: '#aaa',
};

const sectionStyle: React.CSSProperties = {
  marginBottom: 16,
};

const sectionTitleStyle: React.CSSProperties = {
  margin: '0 0 8px',
  fontSize: 13,
  fontWeight: 500,
  color: '#999',
  textTransform: 'uppercase',
  letterSpacing: '0.5px',
};

const intensityRowStyle: React.CSSProperties = {
  display: 'flex',
  alignItems: 'center',
  gap: 10,
};

const intensityValueStyle: React.CSSProperties = {
  fontSize: 16,
  fontWeight: 500,
};

const intensityBadgeStyle: React.CSSProperties = {
  fontSize: 12,
  fontWeight: 600,
  padding: '2px 8px',
  borderRadius: 4,
  color: '#000',
};

const listStyle: React.CSSProperties = {
  listStyle: 'none',
  margin: 0,
  padding: 0,
  display: 'flex',
  flexDirection: 'column',
  gap: 6,
};

const listItemStyle: React.CSSProperties = {
  display: 'flex',
  alignItems: 'center',
  gap: 8,
  fontSize: 13,
};

const fuelLabelStyle: React.CSSProperties = {
  width: 70,
  flexShrink: 0,
};

const barContainerStyle: React.CSSProperties = {
  flex: 1,
  height: 8,
  backgroundColor: '#2a2a3e',
  borderRadius: 4,
  overflow: 'hidden',
};

const barFillStyle: React.CSSProperties = {
  height: '100%',
  backgroundColor: '#4fc3f7',
  borderRadius: 4,
  transition: 'width 0.2s ease',
};

const percentStyle: React.CSSProperties = {
  width: 45,
  textAlign: 'right',
  flexShrink: 0,
  fontVariantNumeric: 'tabular-nums',
};
