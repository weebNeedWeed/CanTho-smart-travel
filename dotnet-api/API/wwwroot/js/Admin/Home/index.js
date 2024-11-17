document.addEventListener("DOMContentLoaded", function () {
    let isPickingLocation = false;
    let tempMarker = null;
    const destinations = JSON.parse(document.getElementById('destination-data').textContent);
    const map = L.map('map').setView([10.0451618, 105.7468535], 13); // Tọa độ trung tâm Cần Thơ
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '© OpenStreetMap'
    }).addTo(map);
    var markers = {};
    var activeCard = null;
    var bounds = L.latLngBounds();

    destinations.forEach(function (dest) {
        if (dest.Latitude && dest.Longitude) {
            var marker = L.marker([dest.Longitude, dest.Latitude]).addTo(map);

            var popupContent = `
                    <div class="popup-content">
                        <h3>${dest.Name}</h3>
                        <p>${dest.Address}</p>
                        <p>Điện thoại: ${dest.PhoneNumber}</p>
                        <p>Email: ${dest.Email}</p>
                    </div>
                `;

            marker.bindPopup(popupContent);
            bounds.extend([dest.Longitude, dest.Latitude]);
            markers[dest.Id] = marker;
        }
    });

    if (bounds.isValid()) {
        map.fitBounds(bounds);
    }

    // Xử lý đóng/mở sidebar
    document.getElementById('sidebarToggle').addEventListener('click', function () {
        document.querySelector('.sidebar').classList.toggle('active');
    });

    // Xử lý tìm kiếm
    document.getElementById('searchInput').addEventListener('input', function (e) {
        var searchText = e.target.value.toLowerCase();
        document.querySelectorAll('.destination-card').forEach(function (card) {
            var name = card.querySelector('h3').textContent.toLowerCase();
            var address = card.querySelector('p').textContent.toLowerCase();
            if (name.includes(searchText) || address.includes(searchText)) {
                card.style.display = '';
            } else {
                card.style.display = 'none';
            }
        });
    });

    // Xử lý click trên card
    document.querySelectorAll('.destination-card').forEach(function (card) {
        card.addEventListener('click', function () {
            // Remove active class from previous card
            if (activeCard) {
                activeCard.classList.remove('active');
            }

            // Add active class to clicked card
            this.classList.add('active');
            activeCard = this;

            var destId = this.dataset.id;
            var destination = destinations.find(d => d.Id == destId);

            if (destination && destination.Latitude && destination.Longitude) {
                map.setView([destination.Latitude, destination.Longitude], 16);
                markers[destId].openPopup();
            }
        });
    });

    // Mở sidebar mặc định
    document.querySelector('.sidebar').classList.add('active');

    let selectedMarker = null;

    // Nút Thêm địa chỉ
    document.getElementById('btnAddLocation').addEventListener('click', () => {
        alert("Hãy chọn vị trí trên bản đồ!");

        // Thêm sự kiện click để chọn địa điểm
        map.on('click', function (e) {
            const { lat, lng } = e.latlng;

            // Xóa marker cũ nếu đã có
            if (selectedMarker) {
                map.removeLayer(selectedMarker);
            }

            // Thêm marker mới tại vị trí được chọn
            selectedMarker = L.marker([lat, lng]).addTo(map);

            // Xác nhận lựa chọn vị trí
            const isConfirmed = confirm("Bạn đã chọn vị trí này. Tiếp tục thêm địa chỉ?");
            if (isConfirmed) {
                // Chuyển đến trang thêm địa điểm với tọa độ
                window.location.href = `/Admin/Destination/AddLocation?latitude=${lat}&longitude=${lng}`;
            }
        });
    });
    })
