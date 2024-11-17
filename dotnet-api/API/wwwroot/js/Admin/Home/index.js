﻿document.addEventListener("DOMContentLoaded", function () {
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
})