// // Lấy các phần tử cần ẩn/hiển thị
// const Infor =document.getElementById('infor');
// const Category =document.getElementById('Category');
// const Products =document.getElementById('Products');
// const USER =document.getElementById('USER');
// const CART =document.getElementById('CART');
// const FORM =document.getElementById('FORM');
// const LOGIN =document.getElementById('LOGIN');
// const LOG_OUT =document.getElementById('OUT');
// const active =document.querySelector('.ACTIONS')
// const Layout_infor=document.querySelector('.admin_right_layout_infor')
// const Layout_category=document.querySelector('.admin_right_layout_category')
// const Layout_product=document.querySelector('.admin_right_layout_product')

// if(Infor){
//     Infor.addEventListener( 'click', function(){
//         Layout_infor.style.display='block'
//         Layout_category.style.display='none'
//         Layout_product.style.display='none'
//     })
// }
// if(Category){
//     Category.addEventListener( 'click', function(){
//         Layout_infor.style.display='none'
//         Layout_category.style.display='block'
//         Layout_product.style.display='none'
//     })
// }
const elements = {
    infor: document.getElementById('infor'),
    category: document.getElementById('Category'),
    products: document.getElementById('Products'),
    user: document.getElementById('USER'),
    cart: document.getElementById('CART'),
    form: document.getElementById('FORM'),
    login: document.getElementById('LOGIN'), 
  };
  
  const layouts = {
    infor: document.querySelector('.admin_right_layout_infor'),
    category: document.querySelector('.admin_right_layout_category'),
    products: document.querySelector('.admin_right_layout_product'),
    user :document.querySelector('.admin_right_layout_user')
  };
  
  function toggleLayout(layout) {
    Object.values(layouts).forEach(layoutItem => {
      layoutItem.style.display = (layoutItem === layout) ? 'block' : 'none';
    });
  }
  
  Object.values(elements).forEach(element => {
    if (element) {
      element.addEventListener('click', function() {
        const layoutKey = this.id.toLowerCase();
        const layout = layouts[layoutKey];
        toggleLayout(layout);
      });
    }
  });