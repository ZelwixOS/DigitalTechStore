import axios from 'axios';

async function deleteRequest<T>(url: string, data: T) {
  return (await axios.delete(url, data)).data;
}

async function deleteFromCart(productId: string) {
  return await deleteRequest(`/api/CustomerLists/cart/${productId}`, null);
}

async function deleteFromWishlist(productId: string) {
  return await deleteRequest(`/api/CustomerLists/wishlist/${productId}`, null);
}

async function deleteCommonCategory(id: string) {
  return await deleteRequest(`/api/CommonCategory/${id}`, null);
}

async function deleteCategory(id: string) {
  return await deleteRequest(`/api/Category/${id}`, null);
}

async function deleteParameterBlock(id: string) {
  return await deleteRequest(`/api/ParameterBlock/${id}`, null);
}

async function deleteParameter(id: string) {
  return await deleteRequest(`/api/TechParameter/${id}`, null);
}

async function deleteParameterValue(id: string) {
  return await deleteRequest(`/api/ParameterValue/${id}`, null);
}

async function deleteProductParameter(id: string) {
  return await deleteRequest(`/api/ProductParameter/${id}`, null);
}

async function deleteProduct(id: string) {
  return await deleteRequest(`/api/Product/${id}`, null);
}

async function deleteRegion(id: number) {
  return await deleteRequest(`/api/Geography/region/${id}`, null);
}

async function deleteCity(id: number) {
  return await deleteRequest(`/api/Geography/city/${id}`, null);
}

async function deleteOutlet(id: number) {
  return await deleteRequest(`/api/Estate/outlet/${id}`, null);
}

async function deleteWarehouse(id: number) {
  return await deleteRequest(`/api/Estate/warehouse/${id}`, null);
}

export {
  deleteFromCart,
  deleteFromWishlist,
  deleteCommonCategory,
  deleteCategory,
  deleteParameterBlock,
  deleteParameter,
  deleteParameterValue,
  deleteProductParameter,
  deleteProduct,
  deleteRegion,
  deleteCity,
  deleteOutlet,
  deleteWarehouse,
};
