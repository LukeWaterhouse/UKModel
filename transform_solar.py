"""Extract compact [lon, lat, capacityMW] arrays from ukpvgeo_geometries.geojson for heatmap.

Grid-aggregates points to ~0.01° cells (~1 km) to reduce 240k raw points
down to ~10-15k aggregated cells for smooth rendering.
"""
import json
from collections import defaultdict

INPUT = "Frontend/src/assets/ukpvgeo_geometries.geojson"
OUTPUT = "Frontend/src/assets/solar-heatmap.json"
GRID_SIZE = 0.01  # ~1 km resolution

with open(INPUT) as f:
    data = json.load(f)

# Aggregate capacity into grid cells keyed by (rounded_lon, rounded_lat)
grid: dict[tuple[float, float], float] = defaultdict(float)

for feature in data["features"]:
    geom = feature.get("geometry")
    props = feature.get("properties", {})

    if not geom or geom.get("type") != "Point":
        continue

    coords = geom.get("coordinates")
    if not coords or len(coords) < 2:
        continue

    lon, lat = coords[0], coords[1]

    capacity = props.get("capacity_repd_MWp") or props.get("capacity_osm_MWp") or 0.001
    try:
        capacity = float(capacity)
    except (ValueError, TypeError):
        capacity = 0.001

    # Snap to grid cell centre
    cell = (round(round(lon / GRID_SIZE) * GRID_SIZE, 4),
            round(round(lat / GRID_SIZE) * GRID_SIZE, 4))
    grid[cell] += capacity

points = [[lon, lat, round(cap, 3)] for (lon, lat), cap in grid.items()]
points.sort(key=lambda p: p[2], reverse=True)

print(f"Aggregated {len(data['features'])} features → {len(points)} grid cells")

with open(OUTPUT, "w") as f:
    json.dump(points, f, separators=(",", ":"))

print(f"Extracted {len(points)} solar points to {OUTPUT}")
print(f"File size: {len(json.dumps(points, separators=(',', ':'))) / 1024:.0f} KB")
