document.addEventListener('DOMContentLoaded', function () {
    // 1. Обновляем счетчик и корзину при загрузке страницы
    updateCartBadge();
    if (document.getElementById('cart-container')) {
        renderCartItems();
    }

    // 2. Логика кнопок "В корзину" (сохраняем её!)
    const buyBtns = document.querySelectorAll('.buy-btn');
    buyBtns.forEach(btn => {
        btn.addEventListener('click', function () {
            const product = {
                name: this.getAttribute('data-name'),
                price: parseInt(this.getAttribute('data-price'))
            };

            let cart = JSON.parse(localStorage.getItem('user_cart')) || [];
            cart.push(product);
            localStorage.setItem('user_cart', JSON.stringify(cart));

            animateAddToCart(this);
            updateCartBadge();
        });
    });
});

// --- ФУНКЦИИ КОРЗИНЫ ---

function updateCartBadge() {
    const cart = JSON.parse(localStorage.getItem('user_cart')) || [];
    const badge = document.getElementById('cart-badge');
    if (badge) badge.innerText = cart.length;
}

function animateAddToCart(button) {
    const originalText = button.innerText;
    button.classList.replace('btn-primary', 'btn-success');
    button.innerText = 'Добавлено! ✅';
    setTimeout(() => {
        button.classList.replace('btn-success', 'btn-primary');
        button.innerText = originalText;
    }, 1200);
}

function renderCartItems() {
    const container = document.getElementById('cart-container');
    const cart = JSON.parse(localStorage.getItem('user_cart')) || [];
    
    if (cart.length === 0) {
        container.innerHTML = '<div class="alert alert-light border text-center py-5"><h3>Корзина пуста</h3></div>';
        return;
    }

    let total = 0;
    let html = '<table class="table align-middle"><thead><tr class="table-dark"><th>Товар</th><th>Цена</th><th></th></tr></thead><tbody>';
    
    cart.forEach((item, index) => {
        total += item.price;
        html += `<tr><td>${item.name}</td><td>${item.price.toLocaleString()} ₽</td>
                 <td><button class="btn btn-sm btn-outline-danger" onclick="removeFromCart(${index})">❌</button></td></tr>`;
    });

    html += `<tr><td class="fw-bold text-end">ИТОГО:</td><td class="fw-bold text-danger">${total.toLocaleString()} ₽</td><td></td></tr></tbody></table>`;
    container.innerHTML = html;
}

// Новое: Очистка всей корзины
window.clearCart = function() {
    if(confirm("Очистить корзину?")) {
        localStorage.removeItem('user_cart');
        location.reload(); 
    }
};

window.removeFromCart = function(index) {
    let cart = JSON.parse(localStorage.getItem('user_cart')) || [];
    cart.splice(index, 1);
    localStorage.setItem('user_cart', JSON.stringify(cart));
    renderCartItems();
    updateCartBadge();
};

// --- ФУНКЦИИ ФИЛЬТРАЦИИ ---

window.filterProducts = function() {
    const activeCats = Array.from(document.querySelectorAll('.filter-check:checked')).map(cb => cb.value);
    document.querySelectorAll('.product-item').forEach(item => {
        const productCat = item.getAttribute('data-category');
        item.style.display = activeCats.includes(productCat) ? 'block' : 'none';
    });
};