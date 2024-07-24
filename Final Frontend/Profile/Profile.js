const fetchWithAuth = async (url, options = {}) => {
  const token = getCookie('token');
  if (!options.headers) {
    options.headers = {};
  }
  if (token) {
    options.headers['Authorization'] = `Bearer ${token}`;
  }

  const response = await fetch(url, options);

  if (response.status === 401) {
    window.location.href = '/Login.html'; 
  }

  return response;
};

const fetchUserProfile = async () => {
  const res = await fetchWithAuth("https://localhost:7029/api/User/ViewProfile", {
    method: "GET"
  });
  const data = await res.json();
  populateUserProfile(data);
};

const populateUserProfile = (data) => {
  document.querySelector('.avatar .text-3xl').textContent = data.firstName[0].toUpperCase();
  document.querySelector('#header-name').innerHTML = `${data.firstName} ${data.lastName}`;
  document.querySelector('#subscription-status').textContent = data.membershipType;
  document.querySelector('#header-email').textContent = data.email;

  // Personal Information section
  document.querySelector('#personal-first-name').textContent = data.firstName;
  document.querySelector('#personal-last-name').textContent = data.lastName;
  document.querySelector('#personal-email').textContent = data.email;
};

let addressList = [];
const fetchAddresses = async () => {
  const res = await fetchWithAuth("https://localhost:7029/api/User/ViewAdressesOfUser/", {
    method: "GET"
  });
  const addresses = await res.json();
  console.log(addresses);
  addressList = addresses;
  populateAddresses(addresses);
};

const fetchOrders = async () => {
  const res = await fetchWithAuth("https://localhost:7029/api/Order/ViewOrders", {
    method: "GET"
  });
  const orders = await res.json();
  console.log(orders);
  populateOrders(orders);
};

const populateAddresses = (addresses) => {
  const addressesList = document.getElementById('addresses-list');
  addressesList.innerHTML = '';
  addresses.forEach((address, index) => {
    const addressItem = document.createElement('li');
    addressItem.className = 'py-2 sm:py-4 border-2 border-b-2 px-5 my-5';
    addressItem.innerHTML = `
      <div class="text-white">
        <p>${address.area}</p>
        <p>${address.city}, ${address.state}</p>
        <p>${address.zipcode}</p>
      </div>
      <div class="mt-5 flex justify-between items-center">
        <div class="btn btn-primary btn-sm bg-blue-800" onclick="showEditAddressModal(${index})">Edit</div>
        <div class="btn btn-accent btn-sm">Remove</div>
        ${address.primaryAdress ? '<div class="kbd kbd-sm">Default Address</div>' : ''}
      </div>
    `;
    addressesList.appendChild(addressItem);
  });
};

const populateOrders = (orders) => {
  const ordersList = document.getElementById('orders-list');
  ordersList.innerHTML = '';
  orders.forEach(order => {
    const orderItem = document.createElement('tr');
    orderItem.className = 'bg-black';
    orderItem.innerHTML = `
      <th scope="row" class="px-6 py-4 font-medium whitespace-nowraptext-white">
        <a href="/Order.html?Id=${order.orderId}">${order.orderId}</a>
      </th>
      <td class="px-6 py-4">${new Date(order.orderedDate).toLocaleDateString()}</td>
      <td class="px-6 py-4">${order.amount}</td>
      <td class="px-6 py-4">${order.paymentType}</td>
      <td class="px-6 py-4">${order.orderStatus}</td>
      <td class="px-6 py-4">${order.rentalOrPermanent}</td>
    `;
    ordersList.appendChild(orderItem);
  });
};

document.addEventListener("DOMContentLoaded", () => {
  console.log("DOM loaded");
  if (getCookie("isLoggedIn") == false || getCookie("isLoggedIn") == null) {
    window.location.href = "/Login.html";
  }
  fetchUserProfile();
  fetchAddresses();
  fetchOrders();
});

const getCookie = (name) => {
  let nameEQ = name + "=";
  let ca = document.cookie.split(';');
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) == ' ') c = c.substring(1, c.length);
    if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
  }
  return null;
};

const showEditAddressModal = (index) => {
  const address = addressList[index];
  document.getElementById('edit-area').value = address.area;
  document.getElementById('edit-city').value = address.city;
  document.getElementById('edit-state').value = address.state;
  document.getElementById('edit-zipcode').value = address.zipcode;
  document.getElementById('edit-primaryAddress').checked = address.primaryAdress;
  document.getElementById('editAddressForm').setAttribute('data-index', index);

  document.getElementById('editAddressModal').showModal();
};

const handleEditAddressForm = async () => {
  const index = document.getElementById('editAddressForm').getAttribute('data-index');
  const address = addressList[index];

  address.area = document.getElementById('edit-area').value;
  address.city = document.getElementById('edit-city').value;
  address.state = document.getElementById('edit-state').value;
  address.zipcode = parseInt(document.getElementById('edit-zipcode').value);
  address.primaryAdress = document.getElementById('edit-primaryAddress').checked;
  console.log(address);

  const res = await fetchWithAuth(`https://localhost:7029/api/User/UpdateAddress`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(address)
  });
  console.log(res);
  if (res.ok) {
    document.getElementById('editAddressModal').close();
    location.reload();
  } else {
    console.error('Failed to update address');
  }
};

const showEditPersonalInfoModal = () => {
  const firstName = document.getElementById('personal-first-name').textContent;
  const lastName = document.getElementById('personal-last-name').textContent;

  document.getElementById('edit-first-name').value = firstName;
  document.getElementById('edit-last-name').value = lastName;

  document.getElementById('editPersonalInfoModal').showModal();
};

const handleEditPersonalInfoForm = async () => {
  const firstName = document.getElementById('edit-first-name').value;
  const lastName = document.getElementById('edit-last-name').value;
  const res = await fetchWithAuth(`https://localhost:7029/api/User/EditProfile`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({ firstName, lastName })
  });

  if (res.ok) {
    document.getElementById('editPersonalInfoModal').close();
    location.reload();
  } else {
    console.error('Failed to update profile');
  }
};

const closeModal = (modalId) => {
  document.getElementById(modalId).close();
};
const showAddAddressModal = () => {
  document.getElementById('add-area').value = '';
  document.getElementById('add-city').value = '';
  document.getElementById('add-state').value = '';
  document.getElementById('add-zipcode').value = '';
  document.getElementById('add-primaryAddress').checked = false;

  document.getElementById('addAddressModal').showModal();
};

const handleAddAddressForm = async () => {
  const area = document.getElementById('add-area').value;
  const city = document.getElementById('add-city').value;
  const state = document.getElementById('add-state').value;
  const zipcode = parseInt(document.getElementById('add-zipcode').value);
  const primaryAdress = document.getElementById('add-primaryAddress').checked;

  const address = { area, city, state, zipcode, primaryAdress };
  console.log(address);

  const res = await fetchWithAuth(`https://localhost:7029/api/User/AddAddress`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(address)
  });

  if (res.ok) {
    document.getElementById('addAddressModal').close();
    location.reload();
  } else {
    console.error('Failed to add address');
  }
};
