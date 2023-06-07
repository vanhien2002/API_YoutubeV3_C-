// Get the button, and when the user clicks on it, execute myFunction
document.getElementById("myBtn").onclick = function () { myFunction() };

/* myFunction toggles between adding and removing the show class, which is used to hide and show the dropdown content */
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}
function IsEmpty() {
    var keySearch = document.getElementById("Keysearch").value;
    if (keySearch == "") {
        alert("Not null");
    }
    return;
}

  //dropdowlist
const dd = document.querySelector('#dropdown-wrapper');
const links = document.querySelectorAll('.dropdown-list a');
const span = document.querySelector('span');

dd.addEventListener('click', function () {
    this.classList.toggle('is-active');
});

links.forEach((element) => {
    element.addEventListener('click', function (evt) {
        span.innerHTML = evt.currentTarget.textContent;
    })
})
//end dropdowlist