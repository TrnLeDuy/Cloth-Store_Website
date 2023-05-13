// Get the quantity and price elements
const quantityElement = document.getElementById("quantity");
const priceElement = document.getElementById("price");

// Add event listeners to update the cart whenever the quantity or price changes
quantityElement.addEventListener("change", updateCart);
priceElement.addEventListener("change", updateCart);

function updateCart() {
    // Get the selected quantity and price from the form
    const quantity = document.getElementById("quantity").value;
    const price = document.getElementById("price").value;

    // Calculate the subtotal and total
    const subtotal = quantity * price;
    const total = subtotal + 10; // add $10 for shipping

    // Update the subtotal and total values in the DOM
    document.getElementById("subtotal").innerHTML = "$" + subtotal.toFixed(2);
    document.getElementById("total").innerHTML = "$" + total.toFixed(2);
}