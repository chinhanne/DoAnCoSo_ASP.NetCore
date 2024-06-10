


document.querySelector(".product-content-right-product-bottom-content-lichsu").style.display = "none";
document.querySelector(".product-content-right-product-bottom-content-noidung").style.display = "none";
document.querySelector(".product-content-right-product-bottom-content-tacgia").style.display = "block";
const bigImg =document.querySelector(".product-content-left-big-img img")
const smallImg= document.querySelectorAll(".product-content-left-small-img img");
smallImg.forEach(function(imgItem,x){
    imgItem.addEventListener("click", function(){
        bigImg.src=imgItem.src;
})
}
)
const tacgia =document.querySelector(".tacgia");
const lichsu =document.querySelector(".lichsu");
const noidung =document.querySelector(".noidung");

if(tacgia){
    tacgia.addEventListener("click",function(){
        document.querySelector(".product-content-right-product-bottom-content-lichsu").style.display = "none";
        document.querySelector(".product-content-right-product-bottom-content-noidung").style.display = "none";
        document.querySelector(".product-content-right-product-bottom-content-tacgia").style.display = "block";
    })
    }

if(lichsu){
    lichsu.addEventListener("click",function(){
        document.querySelector(".product-content-right-product-bottom-content-tacgia").style.display = "none";
        document.querySelector(".product-content-right-product-bottom-content-noidung").style.display = "none";
        document.querySelector(".product-content-right-product-bottom-content-lichsu").style.display = "block";
    })
    }

if(noidung){
    noidung.addEventListener("click",function(){
        document.querySelector(".product-content-right-product-bottom-content-lichsu").style.display = "none";
        document.querySelector(".product-content-right-product-bottom-content-tacgia").style.display = "none";
        document.querySelector(".product-content-right-product-bottom-content-noidung").style.display = "block";
    })
}
const bottom=document.querySelector(".product-content-right-product-bottom-top")
if(bottom){
    bottom.addEventListener("click",function(){
        document.querySelector(".product-content-right-product-bottom-big").classList.toggle("active-b");
    })
}



// const adminRightButton = document.querySelector('.admin_right_button');
// const adminLeft = document.querySelector('.admin_left');
// const adminRight = document.querySelector('.admin_right');
// if(adminRightButton){
//     adminRightButton.addEventListener('click', function() {
//         adminLeft.classList.toggle('display_left');
//         adminRight.classList.toggle('display_right');
//     });
// }
document.getElementById("admin_right_button").addEventListener("click", toggleAdminLeft);

function toggleAdminLeft() {
  var adminLeft = document.querySelector(".admin_left");
  adminLeft.classList.toggle("admin_left_hidden");
}