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

function redirectToLoginPage() {
    // Change the action attribute to "login.html" when the form is submitted
    document.getElementById("registrationForm").action = "login.html";
}

function validateLogin() {
    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;

    // Kullanıcı adı ve şifre kontrolü
    if (username === 'admin' && password === 'admin') {
        // Doğruysa login.html sayfasına yönlendir
        window.location.href = 'index.html';
    } else {
        alert('Invalid username or password. Please try again.');
    }
}
