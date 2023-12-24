/* bu kısım mobil sürümü için searchbar fonksiyonu ekliyor*/
searchForm = document.querySelector(".search-form");
document.querySelector("#search-button").onclick = ()=> {
  searchForm.classList.toggle("active")
}

/*anasayfa sağüstteki user simgesiyle login*/
let loginForm = document.querySelector('.login-form-container');
document.querySelector('#login-btn').onclick = () => {
  loginForm.classList.toggle('active');
}
/*close tuşunun anasayfaya geçmesini sağlayan kısım*/
document.querySelector('#close-login-btn').onclick = () => {
  loginForm.classList.remove('active');
}

/*ekranı indirdiğimizde header2'nin ekranda kalması için*/
window.onscroll = () => {
  if (window.scrollY > 80) {
    document.querySelector(".header .header2").classList.add("active");
  } else {
    document.querySelector(".header .header2").classList.remove("active");
  }
};

/* window.onload, bu olayın sayfa tamamen yüklendiğinde çalışması için */
window.onload = () => {
  if (window.scrollY > 80) {
    document.querySelector(".header .header2").classList.add("active");
  } else {
    document.querySelector(".header .header2").classList.remove("active");
  }
};
