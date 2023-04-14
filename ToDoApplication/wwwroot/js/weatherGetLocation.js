
var locationInput = document.getElementById("location");

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}

function showPosition(position) {
    var geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                for (var i = 0; i < results[0].address_components.length; i++) {
                    for (var j = 0; j < results[0].address_components[i].types.length; j++) {
                        if (results[0].address_components[i].types[j] == "locality") {
                            locationInput.value = results[0].address_components[i].long_name;
                            return;
                        }
                    }
                }
                showError("No locality found in the address.");
            } else {
                showError("No results found");
            }
        } else {
            showError("Geocoder failed due to: " + status);
        }
    });
}

function showError(error) {
    alert(error);
}

document.getElementById("location-btn").addEventListener("click", getLocation);

