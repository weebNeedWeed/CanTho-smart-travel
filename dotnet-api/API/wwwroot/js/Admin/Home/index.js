// site.js
function initMap(destinations) {
    // Khởi tạo map
    const map = L.map('map').setView([10.0452, 105.7469], 13);
    var markers = {};

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);


    // Thêm markers
    destinations.forEach(dest => {
        var marker = L.marker([dest.Latitude, dest.Longitude]).addTo(map);
        markers[dest.Id] = marker;
        marker.on('click', () => showInfoCard(dest));
    });

    // Xử lý tìm kiếm
    const searchInput = document.getElementById('searchInput');
    searchInput.addEventListener('input', (e) => {
        const searchText = e.target.value.toLowerCase();

        markers.forEach(({ marker, data }) => {
            if (data.Name.toLowerCase().includes(searchText)) {
                marker.setOpacity(1);
            } else {
                marker.setOpacity(0.2);
            }
        });
    });
}

// Hiển thị thông tin địa điểm
function showInfoCard(destination) {
    const card = document.getElementById('infoCard');

    // Cập nhật nội dung
    document.getElementById('destinationImage').src = destination.Photos[0] || 'placeholder.jpg';
    document.getElementById('destinationName').textContent = destination.Name;
    document.getElementById('destinationAddress').textContent = destination.Address;
    document.getElementById('destinationDescription').textContent = destination.Description;

    // Hiển thị amenities
    const amenitiesContainer = document.getElementById('destinationAmenities');
    amenitiesContainer.innerHTML = '';
    destination.Amenities.forEach(amenity => {
        const span = document.createElement('span');
        span.className = 'tag';
        span.textContent = amenity;
        amenitiesContainer.appendChild(span);
    });

    // Hiển thị tags
    const tagsContainer = document.getElementById('destinationTags');
    tagsContainer.innerHTML = '';
    destination.Tags.forEach(tag => {
        const span = document.createElement('span');
        span.className = 'tag';
        span.textContent = tag;
        tagsContainer.appendChild(span);
    });

    // Hiển thị giờ mở cửa
    document.getElementById('destinationHours').textContent = destination.OpeningHours;

    // Hiển thị thông tin liên hệ
    document.getElementById('destinationContact').innerHTML = `
        <div>Điện thoại: ${destination.PhoneNumber}</div>
        <div>Email: ${destination.Email}</div>
    `;

    card.style.display = 'block';
}

// Đóng card thông tin
function closeInfoCard() {
    document.getElementById('infoCard').style.display = 'none';
}