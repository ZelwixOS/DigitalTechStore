import axios from 'axios';

async function put<T>(url: string, data: T) {
  return (await axios.put(url, data)).data;
}

async function updateCartItem(count: number, productId: string) {
  return await put(`/api/CustomerLists/cart/`, { count: count, productId: productId });
}

async function updateCommonCategory(id: string, name: string, description: string) {
  const data = { id, name, description };
  return await put(`/api/CommonCategory/`, data);
}

async function updateCategory(
  id: string,
  name: string,
  description: string,
  deliveryPrice: number,
  commonCategoryId: string,
) {
  const data = { id, name, description, deliveryPrice, commonCategoryId };
  return await put(`/api/Category/`, data);
}

export { updateCartItem, updateCommonCategory, updateCategory };
