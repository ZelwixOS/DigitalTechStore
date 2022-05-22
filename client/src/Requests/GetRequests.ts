import axios from 'axios';

import Category from 'src/Types/Category';
import CategoryAllParameterBlocks from 'src/Types/CategoryAllParameterBlocks';
import CommonCategory from 'src/Types/CommonCategory';
import FilterValue from 'src/Types/FilterValue';
import Outlet from 'src/Types/Outlet';
import Parameter from 'src/Types/Parameter';
import ParameterBlock from 'src/Types/ParameterBlock';
import Product from 'src/Types/Product';
import Purchase from 'src/Types/Purchase';
import Region from 'src/Types/Region';
import Sorting from 'src/Types/Sorting';
import Warehouse from 'src/Types/Warehouse';

async function getRequest(url: string) {
  return (await axios.get(url)).data;
}

async function getProducts(currentPage: number, itemsOnPage: number, sortType: string, price: number[]) {
  const sortparams: Sorting = sortTypeParsing(sortType);
  const params = new URLSearchParams(location.search);
  const search = params.get('search');
  let url = `/api/Product?PageNumber=${currentPage}&ItemsOnPage=${itemsOnPage}&SortingType=${sortparams.type}&ReverseSorting=${sortparams.reverse}&MinPrice=${price[0]}&MaxPrice=${price[1]}`;
  if (search) {
    url += `&search=${search}`;
  }

  return await getRequest(url);
}

async function getSearchedProducts(currentPage: number, itemsOnPage: number, sortType: string, search: string) {
  const sortparams: Sorting = sortTypeParsing(sortType);
  const url = `/api/Product?PageNumber=${currentPage}&ItemsOnPage=${itemsOnPage}&SortingType=${sortparams.type}&ReverseSorting=${sortparams.reverse}&search=${search}`;
  return await getRequest(url);
}

async function getCart() {
  const url = `/api/CustomerLists/cart`;
  return await getRequest(url);
}

async function getCartUnsigned() {
  const items = localStorage.getItem('cartItems');
  const url = `/api/CustomerLists/cart/unsigned?productIds=${items}`;
  return await getRequest(url);
}

async function getWishlist() {
  const url = `/api/CustomerLists/wishlist`;
  return await getRequest(url);
}

async function getCategories(commonCategory: string) {
  return (await getRequest(`/api/Category/${commonCategory}`)) as Category[];
}

async function getAllCategories() {
  return (await getRequest(`/api/Category/`)) as Category[];
}

async function getCommonCategories() {
  return (await getRequest('/api/CommonCategory')) as CommonCategory[];
}

async function getReviews(productId: string) {
  return await getRequest(`/api/Review/${productId}`);
}

async function getProduct(id: string) {
  const cityId = localStorage.getItem('cityId');
  return (await getRequest(`/api/Product/${id}?cityId=${cityId}`)) as Product;
}

async function getParameters(id: string) {
  return (await getRequest(`/api/Product/parameters/${id}`)) as ParameterBlock[];
}

async function getRegions() {
  return (await getRequest('/api/Geography/regions')) as Region[];
}

async function getAllRegions() {
  return (await getRequest('/api/Geography/allRegions')) as Region[];
}

async function getCategoryById(id: string) {
  return (await getRequest(`/api/Category/id/${id}`)) as Category;
}

async function getProductsOfCategory(
  categoryName: string,
  currentPage: number,
  itemsOnPage: number,
  sortType: string,
  price: number[],
  filters: FilterValue[],
) {
  const sortparams: Sorting = sortTypeParsing(sortType);
  let url = `/api/Category/name/${categoryName}?PageNumber=${currentPage}&ItemsOnPage=${itemsOnPage}&SortingType=${sortparams.type}&ReverseSorting=${sortparams.reverse}&MinPrice=${price[0]}&MaxPrice=${price[1]}`;

  for (const filter of filters) {
    if (filter.range) {
      if (filter.minValue || filter.maxValue) {
        if (!filter.minValue) {
          filter.minValue = '0';
        }
        if (!filter.maxValue) {
          filter.maxValue = '0';
        }
        url += `&${filter.id}=${filter.minValue},${filter.maxValue}`;
      }
    } else {
      if (filter.itemIds && filter.itemIds.length > 0) {
        url += `&${filter.id}=${filter.itemIds?.join(',')}`;
      }
    }
  }

  return await getRequest(url);
}

function sortTypeParsing(sortType: string): Sorting {
  if (sortType.includes('reverse')) {
    return { type: sortType.split(' ')[0], reverse: true };
  } else {
    return { type: sortType.split(' ')[0], reverse: false };
  }
}

async function getPurchases() {
  return (await getRequest('/api/Purchase')) as Purchase[];
}

async function getCommonCategory(id: string) {
  return (await getRequest(`/api/CommonCategory/${id}`)) as CommonCategory;
}

async function getCategoryParamBlocks(id: string) {
  return (await getRequest(`/api/Category/parameterBlocks/${id}`)) as CategoryAllParameterBlocks;
}

async function getParamBlocks() {
  return (await getRequest(`/api/ParameterBlock`)) as ParameterBlock[];
}

async function getParameterBlock(id: string) {
  return (await getRequest(`/api/ParameterBlock/${id}`)) as ParameterBlock;
}

async function getTechParameters() {
  return (await getRequest(`/api/TechParameter`)) as Parameter[];
}

async function getTechListParameters() {
  return (await getRequest(`/api/TechParameter/lists`)) as Parameter[];
}

async function getParameter(id: string) {
  return (await getRequest(`/api/TechParameter/${id}`)) as Parameter;
}

async function getOutlets(id: number) {
  return (await getRequest(`/api/Estate/outlets/${id}`)) as Outlet[];
}

async function getWarehouses(id: number) {
  return (await getRequest(`/api/Estate/warehouses/city/${id}`)) as Warehouse[];
}

async function getWorkers() {
  return await getRequest(`/api/Account/GetWorkers`);
}

async function getWorker(id: string) {
  return await getRequest(`/api/Account/${id}`);
}

async function getParameterValues() {
  return await getRequest(`/api/ParameterValue`);
}

async function getParameterValue(id: string) {
  return await getRequest(`/api/ParameterValue/${id}`);
}

async function getProductParameter() {
  return await getRequest(`/api/ProductParameter`);
}

export default getRequest;

export {
  getProducts,
  getRequest,
  getCategories,
  getCommonCategories,
  getProductsOfCategory,
  getProduct,
  getCart,
  getWishlist,
  getReviews,
  getParameters,
  getRegions,
  getAllRegions,
  getCartUnsigned,
  getPurchases,
  getAllCategories,
  getCommonCategory,
  getCategoryById,
  getCategoryParamBlocks,
  getParamBlocks,
  getParameterValue,
  getParameterValues,
  getParameterBlock,
  getTechParameters,
  getTechListParameters,
  getParameter,
  getWorkers,
  getOutlets,
  getWarehouses,
  getWorker,
  getProductParameter,
  getSearchedProducts,
};
