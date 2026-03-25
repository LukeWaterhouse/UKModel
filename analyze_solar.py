import json

with open('Frontend/src/assets/ukpvgeo_geometries.geojson') as f:
    data = json.load(f)

features = data['features']

# Extract meaningful solar installations: power_type=plant OR has REPD ID with capacity
solar_farms = []
for f in features:
    p = f['properties']
    coords = f['geometry'].get('coordinates', []) if f.get('geometry') else []
    if not coords or len(coords) < 2:
        continue
    
    cap_repd = p.get('capacity_repd_MWp')
    cap_osm = p.get('capacity_osm_MWp')
    capacity = cap_repd or cap_osm or 0
    if capacity:
        capacity = float(capacity)
    
    name = p.get('repd_site_name') or p.get('osm_name')
    is_plant = p.get('osm_power_type') == 'plant'
    has_repd = p.get('repd_id') is not None
    
    # Include if: it's a plant, or has REPD registration with capacity >= 0.1 MWp
    if is_plant or (has_repd and capacity >= 0.1):
        entry = {
            'lat': coords[1],
            'lon': coords[0],
            'name': name,
            'capacityMWp': round(capacity, 2) if capacity else None,
            'located': p.get('located'),
            'status': p.get('repd_status'),
            'operationalDate': p.get('repd_operational_date'),
        }
        solar_farms.append(entry)

solar_farms.sort(key=lambda x: x.get('capacityMWp') or 0, reverse=True)

print(f'Solar farms extracted: {len(solar_farms)}')
print(f'With capacity: {sum(1 for s in solar_farms if s["capacityMWp"])}')
print(f'With name: {sum(1 for s in solar_farms if s["name"])}')
print(f'Top 10:')
for s in solar_farms[:10]:
    print(f'  {s["name"]} - {s["capacityMWp"]} MWp')

with open('Frontend/src/assets/uk-solar-farms.json', 'w') as f:
    json.dump(solar_farms, f, separators=(',', ':'))

import os
size = os.path.getsize('Frontend/src/assets/uk-solar-farms.json')
print(f'Output size: {size / 1024:.1f} KB')
