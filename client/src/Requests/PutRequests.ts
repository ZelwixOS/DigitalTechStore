import axios from 'axios';

import ParameterCreateRequest from 'src/Types/ParameterCreateRequest';

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

async function updateParameterBlock(id: string, name: string) {
  const data = { id, name };
  return await put(`/api/ParameterBlock/`, data);
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

async function updateParameter(
  id: string,
  name: string,
  important: boolean,
  range: boolean,
  minValue: number,
  maxValue: number,
  parameterBlockId: string,
) {
  const data = { id, name, important, range, minValue, maxValue, parameterBlockId };
  return await put(`/api/TechParameter/`, data);
}

async function updateWorker(
  id: string,
  email: string,
  phoneNumber: string,
  firstName: string,
  secondName: string,
  roleName: string,
  outletId: number,
  warehouseId: number,
  outletWorker: boolean,
) {
  const data = {
    id,
    email,
    phoneNumber,
    firstName,
    secondName,
    roleName,
    outletId: outletWorker ? outletId : null,
    warehouseId: outletWorker ? null : warehouseId,
  };
  return await put(`/api/Account/`, data);
}

async function updateParameterValue(id: string, techParameterId: string, value: string) {
  const data = { id, value, techParameterId };
  return await put(`/api/ParameterValue`, data);
}

async function updateProduct(
  id: string,
  name: string,
  description: string,
  price: number,
  priceWithoutDiscount: number,
  categoryId: string,
  vendorCode: string,
  picFile: File | null,
  parameters?: ParameterCreateRequest[],
) {
  const formData = new FormData();
  formData.append('id', id);
  formData.append('name', name);
  formData.append('description', description);
  formData.append('price', `${price}`);
  formData.append('priceWithoutDiscount', `${priceWithoutDiscount}`);
  formData.append('categoryId', categoryId);
  formData.append('vendorCode', vendorCode);
  if (picFile) {
    formData.append('picFile', picFile);
  }

  const parameterString = JSON.stringify(parameters);
  formData.append(`parameterString`, parameterString);

  return await put(`/api/Product`, formData);
}

async function updateRegion(id: string, name: string) {
  const data = { id, name };
  return await put(`/api/Geography/region`, data);
}

async function updateCity(id: number, name: string, regionId: number) {
  const data = { id, name, regionId };
  return await put(`/api/Geography/city`, data);
}

async function updateOutlet(
  id: number,
  name: string,
  cityId: number,
  streetName: string,
  building: string,
  postalCode: string,
  noteForUser: string,
) {
  const data = { id, name, cityId, streetName, building, postalCode, noteForUser };
  return await put(`/api/Estate/outlet`, data);
}

async function updateWarehouse(
  id: number,
  name: string,
  cityId: number,
  streetName: string,
  building: string,
  postalCode: string,
) {
  const data = { id, name, cityId, streetName, building, postalCode };
  return await put(`/api/Estate/warehouse`, data);
}

async function updatePurchaseStatus(id: string, status: number) {
  const data = { id, status };
  return await put(`/api/Purchase`, data);
}

async function cancelPurchaseStatus(id: string) {
  return await put(`/api/Purchase/${id}`, null);
}

export {
  updateCartItem,
  updateCommonCategory,
  updateCategory,
  updateParameterBlock,
  updateParameter,
  updateWorker,
  updateParameterValue,
  updateProduct,
  updateRegion,
  updateCity,
  updateOutlet,
  updateWarehouse,
  updatePurchaseStatus,
  cancelPurchaseStatus,
};
