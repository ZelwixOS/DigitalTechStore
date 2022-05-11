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

async function updateParameterValue(id: string, techParameterIdFk: string, value: string) {
  const data = { id, value, techParameterIdFk };
  return await put(`/api/ParameterValue`, data);
}

export {
  updateCartItem,
  updateCommonCategory,
  updateCategory,
  updateParameterBlock,
  updateParameter,
  updateWorker,
  updateParameterValue,
};
