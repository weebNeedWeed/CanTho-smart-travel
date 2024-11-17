
    function loadWards(districtId) {
        const wardSelect = document.getElementById('CommuneWardId');
    wardSelect.innerHTML = '<option value="" disabled selected>Đang tải...</option>';

    fetch(`/Admin/Destination/GetWardsByDistrict?districtId=${districtId}`)
            .then(response => response.json())
            .then(data => {
            wardSelect.innerHTML = '<option value="" disabled selected>Chọn phường</option>';
                    data.forEach(ward => {
                        const option = document.createElement('option');
            option.value = ward.id;
            option.textContent = ward.name;
            wardSelect.appendChild(option);
                        });
                    })
            .catch(error => {
                console.error('Lỗi khi tải phường:', error);
            wardSelect.innerHTML = '<option value="" disabled selected>Lỗi tải dữ liệu</option>';
            });
    }
