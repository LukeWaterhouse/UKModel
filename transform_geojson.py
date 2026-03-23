import json
import os
from pyproj import Transformer
from shapely.geometry import shape, mapping
from shapely.ops import transform

# Convert from British National Grid to WGS84
transformer = Transformer.from_crs("EPSG:27700", "EPSG:4326", always_xy=True)

def reproject(geom):
    return transform(transformer.transform, geom)

with open("/Users/luke.waterhouse/Downloads/gb-dno-license-areas-20240503-as-geojson.geojson") as f:
    data = json.load(f)

# Map DNO Area names to the app's GridRegionType names
AREA_TO_REGION = {
    "East England": ("EastEngland", "East England"),
    "East Midlands": ("EastMidlands", "East Midlands"),
    "London": ("London", "London"),
    "North Wales, Merseyside and Cheshire": ("NorthWales", "North Wales"),
    "West Midlands": ("WestMidlands", "West Midlands"),
    "North East England": ("NorthEastEngland", "North East England"),
    "North West England": ("NorthWestEngland", "North West England"),
    "North Scotland": ("NorthScotland", "North Scotland"),
    "South East England": ("SouthEastEngland", "South East England"),
    "South Wales": ("SouthWales", "South Wales"),
    "South West England": ("SouthWestEngland", "South West England"),
    "Yorkshire": ("Yorkshire", "Yorkshire"),
    "South and Central Scotland": ("SouthScotland", "South Scotland"),
    "Southern England": ("SouthEngland", "South England"),
}

features = []

for f in data["features"]:
    area = f["properties"]["Area"]
    if area not in AREA_TO_REGION:
        print(f"  Skipping unknown area: {area}")
        continue

    region_type, shortname = AREA_TO_REGION[area]
    geom = shape(f["geometry"])

    # Reproject from BNG to WGS84
    geom_wgs84 = reproject(geom)

    # Simplify (~0.004 degrees ≈ 400m, good balance of detail vs file size)
    simplified = geom_wgs84.simplify(0.004, preserve_topology=True)

    feature = {
        "type": "Feature",
        "properties": {
            "regionType": region_type,
            "shortname": shortname,
        },
        "geometry": mapping(simplified),
    }
    features.append(feature)

result = {"type": "FeatureCollection", "features": features}

output_path = "/Users/luke.waterhouse/Repos/UKModel/Frontend/src/assets/uk-grid-regions.geojson"
with open(output_path, "w") as f:
    json.dump(result, f)

size = os.path.getsize(output_path)
print(f"Written {len(features)} features, {size} bytes ({size/1024:.1f} KB)")
for feat in features:
    print(f"  {feat['properties']['regionType']}: {feat['geometry']['type']}")
