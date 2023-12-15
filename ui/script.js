/* bu kısım mobil sürümü için searchbar fonksiyonu ekliyor*/
searchForm = document.querySelector(".search-form");
document.querySelector("#search-button").onclick = ()=> {
  searchForm.classList.toggle("active")
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
