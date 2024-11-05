export function getRandomColor(index?: number): string {
  const colors = [
    "#007AFF", // Electric Blue
    "#FF0000", // Bright Red
    "#39FF14", // Neon Green
    "#FF00FF", // Magenta
    "#FFA500", // Bright Orange
    "#00FFFF", // Cyan
    "#DFFF00", // Lime Yellow
    "#FF1493", // Vivid Pink
    "#8A2BE2", // Bright Purple
    "#30D5C8", // Turquoise
    "#FFFF00", // Bright Yellow
    "#FF69B4", // Hot Pink
  ];

  if (typeof index !== "undefined") {
    return colors[index];
  }

  const randomIndex = Math.floor(Math.random() * colors.length);
  return colors[randomIndex];
}

// Vietnamese translation mappings for OSRM instructions
const vietnameseTranslations = {
  // Basic maneuver types
  depart: "Xuất phát",
  arrive: "Đã đến nơi",
  turn: "Rẽ",
  "new name": "Đi tiếp",
  merge: "Nhập làn",
  "on ramp": "Đi vào đường dẫn",
  "off ramp": "Rời đường dẫn",
  fork: "Rẽ nhánh",
  end: "Đến nơi",
  continue: "Đi tiếp",
  roundabout: "Đi vào vòng xuyến",
  rotary: "Đi vào bùng binh",
  "roundabout turn": "Tại vòng xuyến",
  notification: "Chú ý",
  "use lane": "Sử dụng làn đường",

  // Detailed maneuver types
  "exit roundabout": "Thoát vòng xuyến",
  "exit rotary": "Thoát bùng binh",
  stay: "Đi tiếp",
  waypoint: "Điểm dừng",
  "enter roundabout": "Vào vòng xuyến",
  "arrive at waypoint": "Đến điểm dừng",
  "depart from waypoint": "Rời điểm dừng",

  // Modifiers - Basic directions
  right: "phải",
  left: "trái",
  straight: "thẳng",
  "slight right": "phải nhẹ",
  "slight left": "trái nhẹ",
  "sharp right": "phải gấp",
  "sharp left": "trái gấp",
  uturn: "quay đầu",

  // Modifiers - Complex directions
  "straight ahead": "đi thẳng phía trước",
  "keep right": "giữ bên phải",
  "keep left": "giữ bên trái",
  "keep straight": "giữ thẳng",

  // Highway related
  motorway: "đường cao tốc",
  motorway_link: "đường dẫn cao tốc",
  trunk: "đường trục chính",
  trunk_link: "đường dẫn trục chính",
  primary: "đường chính",
  primary_link: "đường dẫn chính",
  secondary: "đường phụ",
  secondary_link: "đường dẫn phụ",
  tertiary: "đường nhánh",
  tertiary_link: "đường dẫn nhánh",
  unclassified: "đường chưa phân loại",
  residential: "đường khu dân cư",
  service: "đường nội bộ",
  living_street: "đường nội khu",

  // Lane guidance
  "in roundabout": "trong vòng xuyến",
  exit: "rẽ ra",

  // Combined instructions
  "merge left": "nhập làn bên trái",
  "merge right": "nhập làn bên phải",
  "merge straight": "nhập làn thẳng",
  "take exit": "rẽ ra",
  "take the exit": "rẽ ra",
  enter: "đi vào",

  // Special cases
  destination: "đến",
  destinations: "các điểm đến",
  named: "có tên",
  unnamed: "không tên",
  ref: "số",

  // Road types
  path: "đường mòn",
  footway: "đường đi bộ",
  cycleway: "đường xe đạp",
  pedestrian: "đường người đi bộ",
  track: "đường đất",
  steps: "bậc thang",

  // Direction indicators
  north: "hướng bắc",
  south: "hướng nam",
  east: "hướng đông",
  west: "hướng tây",
  northeast: "hướng đông bắc",
  northwest: "hướng tây bắc",
  southeast: "hướng đông nam",
  southwest: "hướng tây nam",

  // Additional common phrases
  towards: "hướng về",
  near: "gần",
  onto: "vào",
  along: "dọc theo",
  across: "băng qua",
  at: "tại",
  via: "qua",

  // Traffic related
  "traffic light": "đèn giao thông",
  "traffic circle": "vòng xuyến",
  junction: "ngã tư",
  crossing: "điểm giao cắt",
  intersection: "giao lộ",

  // Distance related phrases
  for: "trong",
  until: "cho đến",
  then: "sau đó",

  // Error messages
  "no route found": "không tìm thấy đường đi",
  "route not found": "không tìm thấy tuyến đường",
  "unable to find route": "không thể tìm tuyến đường",

  // Additional contextual translations
  "toll road": "đường thu phí",
  ferry: "phà",
  "ferry terminal": "bến phà",
  "parking lot": "bãi đỗ xe",
  "parking entrance": "lối vào bãi đỗ xe",
  "gas station": "trạm xăng",
  "rest area": "khu nghỉ dưỡng",
  "service area": "khu dịch vụ",
};

// Helper function to format distance in Vietnamese
function formatDistanceVi(meters) {
  return meters >= 1000
    ? `${(meters / 1000).toFixed(1)} km`
    : `${Math.round(meters)} mét`;
}

// Helper function to format duration in Vietnamese
function formatDurationVi(seconds) {
  const hours = Math.floor(seconds / 3600);
  const minutes = Math.floor((seconds % 3600) / 60);

  let result = "";
  if (hours > 0) {
    result += `${hours} giờ `;
  }
  if (minutes > 0 || hours === 0) {
    result += `${minutes} phút`;
  }
  return result.trim();
}

// Function to translate a single step to Vietnamese
function translateStepToVietnamese(step: any) {
  // Get the basic instruction type
  const instruction =
    vietnameseTranslations[step.instruction.toLowerCase()] || step.instruction;

  // Get the modifier if it exists
  const modifier = step.modifier
    ? vietnameseTranslations[step.modifier.toLowerCase()] || step.modifier
    : "";

  // Build the complete instruction
  let vietnameseInstruction = "";

  // Special case for roundabouts
  if (step.instruction.toLowerCase().includes("roundabout")) {
    vietnameseInstruction = `${instruction} ${modifier}`;
  } else {
    // Regular case
    if (modifier) {
      vietnameseInstruction = `${instruction} ${modifier}`;
    } else {
      vietnameseInstruction = instruction;
    }
  }

  // Add road name if available
  const roadName = step.name === "unnamed road" ? "đường không tên" : step.name;

  // Construct the full instruction with distance and duration
  return {
    ...step,
    vietnameseInstruction: `${vietnameseInstruction} trên ${roadName}`,
    vietnameseDistance: formatDistanceVi(step.distance),
    vietnameseDuration: formatDurationVi(step.duration),
  };
}

// Modified getRoute function with Vietnamese translation
export function getRoute(data: any) {
  // Process steps with Vietnamese translation
  const routeSteps = data.routes[0].legs[0].steps.map((step) => {
    const processedStep = {
      instruction: step.maneuver.type,
      modifier: step.maneuver.modifier || "",
      distance: step.distance,
      duration: step.duration,
      name: step.name || "unnamed road",
      mode: step.mode,
      coordinates: step.maneuver.location,
      startLocation: {
        lat: step.maneuver.location[1],
        lng: step.maneuver.location[0],
      },
    };
    return translateStepToVietnamese(processedStep);
  });

  return {
    distance: data.routes[0].distance,
    duration: data.routes[0].duration,
    vietnameseDistance: formatDistanceVi(data.routes[0].distance),
    vietnameseDuration: formatDurationVi(data.routes[0].duration),
    steps: routeSteps,
  };
}
