import axios from 'axios';

import ItemOfPurchase from 'src/Types/ItemOfPurchase';
import ParameterBlock from 'src/Types/ParameterBlock';
import ParameterBlockCreateRequest from 'src/Types/ParameterBlockCreateRequest';
import ParameterCreateRequest from 'src/Types/ParameterCreateRequest';
import PurchaseRequest from 'src/Types/PurchaseRequest';

async function post<T>(url: string, data: T) {
  return (await axios.post(url, data)).data;
}

async function addToCart(productId: string, count: number) {
  return await post('/api/CustomerLists/cart/', { productId: productId, count: count });
}

async function addToWishlist(productId: string) {
  return await post(`/api/CustomerLists/wishlist/${productId}/`, null);
}

async function createReview(productId: string, mark: number, description: string) {
  return await post(`/api/Review/`, { productId: productId, mark: mark, description: description });
}

async function getPrepurchaseInfo(items: ItemOfPurchase[]) {
  const cityId = localStorage.getItem('cityId');
  const data = { cityId: cityId, purchaseItems: items };
  return await post(`/api/Purchase/preinfo`, data);
}

async function createPurchase(data: PurchaseRequest) {
  const cityId = localStorage.getItem('cityId');
  data.cityId = parseInt(cityId as string);
  if (data.delivery) {
    data.delivery.cityId = parseInt(cityId as string);
  }

  return await post(`/api/Purchase/`, data);
}

async function createCommonCategory(name: string, description: string) {
  const data = { name: name, description: description };
  return await post(`/api/CommonCategory`, data);
}

async function createCategory(name: string, description: string, deliveryPrice: number, commonCategoryId: string) {
  const data = { name: name, description: description, deliveryPrice, commonCategoryId };
  return await post(`/api/Category`, data);
}

async function setCategoryParameterBlocks(categoryId: string, blocks: ParameterBlockCreateRequest[]) {
  return await post(`/api/ParameterBlock/setMany/${categoryId}`, blocks);
}

async function createParameterBlock(name: string) {
  const data = { name };
  return await post(`/api/ParameterBlock/`, data);
}

async function banUser(id: string) {
  return await post(`/api/Account/Ban/${id}`, null);
}

async function unbanUser(id: string) {
  return await post(`/api/Account/Unban/${id}`, null);
}

async function registerWorker(
  login: string,
  password: string,
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
    login,
    password,
    email,
    phoneNumber,
    firstName,
    secondName,
    roleName,
    outletId: outletWorker ? outletId : null,
    warehouseId: outletWorker ? null : warehouseId,
  };
  return await post(`/api/Account/RegisterWorker/`, data);
}

async function createParameter(
  name: string,
  important: boolean,
  range: boolean,
  minValue: number,
  maxValue: number,
  parameterBlockId: string,
) {
  const data = { name, important, range, minValue, maxValue, parameterBlockId };
  return await post(`/api/TechParameter/`, data);
}

async function createParameterValue(techParameterId: string, value: string) {
  const data = { value, techParameterId };
  return await post(`/api/ParameterValue`, data);
}

async function createProductParameter(
  productId: string,
  value: number | null,
  parameterId: string,
  parameterValueId: string | null,
) {
  const data = { productId, value, parameterId, parameterValueId };
  return await post(`/api/ProductParameter`, data);
}

async function cloneProduct(productId: string) {
  return await post(`/api/Product/clone/${productId}`, null);
}

async function publishProduct(productId: string) {
  return await post(`/api/Product/publish/${productId}`, null);
}

async function unpublishProduct(productId: string) {
  return await post(`/api/Product/unpublish/${productId}`, null);
}

async function createProduct(
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

  return await post(`/api/Product`, formData);
}

async function createRegion(name: string) {
  const data = { name };
  return await post(`/api/Geography/region`, data);
}

async function createCity(name: string, regionId: number) {
  const data = { name, regionId };
  return await post(`/api/Geography/city`, data);
}

async function createOutlet(
  name: string,
  cityId: number,
  streetName: string,
  building: string,
  postalCode: string,
  noteForUser: string,
  phoneNumber: string,
) {
  const data = { name, cityId, streetName, building, postalCode, noteForUser, phoneNumber };
  return await post(`/api/Estate/outlet`, data);
}

async function createWarehouse(
  name: string,
  cityId: number,
  streetName: string,
  building: string,
  postalCode: string,
  phoneNumber: string,
) {
  const data = { name, cityId, streetName, building, postalCode, phoneNumber };
  return await post(`/api/Estate/warehouse`, data);
}

async function banReview(id: string) {
  return await post(`/api/Review/Ban/${id}`, null);
}

export {
  addToCart,
  addToWishlist,
  createReview,
  getPrepurchaseInfo,
  createPurchase,
  createCommonCategory,
  createCategory,
  setCategoryParameterBlocks,
  createParameterBlock,
  createParameter,
  banUser,
  unbanUser,
  registerWorker,
  createParameterValue,
  createProductParameter,
  cloneProduct,
  publishProduct,
  unpublishProduct,
  createProduct,
  createRegion,
  createCity,
  createOutlet,
  createWarehouse,
  banReview,
};
