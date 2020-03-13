//SaleSettings
var slider1 = document.getElementById("maxSale");
var slider2 = document.getElementById("saleStep");
var output1 = document.getElementById("demo1");
var output2 = document.getElementById("demo2");
if (slider1) {
    output1.innerHTML = slider1.value;

    slider1.oninput = function () {
        output1.innerHTML = this.value;

        if (slider2.value > this.value) slider2.value = this.value;

        slider2.setAttribute("max", this.value);

        output2.innerHTML = slider2.value;
    }
}
if (slider2) {
    output2.innerHTML = slider2.value;

    slider2.oninput = function () {
        output2.innerHTML = this.value;
    }
}

//HotTour
function checkHot(event, tourId) {
    $('#tourId').val(tourId);
    $('#sendTour').submit();
}

//Block User
function checkBlock(event, userId) {
    $('#settings' + userId).submit();
}