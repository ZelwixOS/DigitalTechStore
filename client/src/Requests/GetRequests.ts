import axios from 'axios';

import Category from 'src/Types/Category';
import CommonCategory from 'src/Types/CommonCategory';
import ParameterBlock from 'src/Types/ParameterBlock';
import Product from 'src/Types/Product';
import Sorting from 'src/Types/Sorting';

async function getRequest(url: string) {
  return (await axios.get(url)).data;
}

async function getProducts(currentPage: number, itemsOnPage: number, sortType: string, price: number[]) {
  const sortparams: Sorting = sortTypeParsing(sortType);
  const url = `/api/Product?PageNumber=${currentPage}&ItemsOnPage=${itemsOnPage}&SortingType=${sortparams.type}&ReverseSorting=${sortparams.reverse}&MinPrice=${price[0]}&MaxPrice=${price[1]}`;
  return await getRequest(url);
}

async function getCart() {
  const url = `/api/CustomerLists/cart`;
  return await getRequest(url);
}

async function getWishlist() {
  const url = `/api/CustomerLists/wishlist`;
  return await getRequest(url);
}

async function getCategories(commonCategory: string) {
  return (await getRequest(`/api/Category/${commonCategory}`)) as Category[];
}

async function getCommonCategories() {
  return (await getRequest('/api/CommonCategory')) as CommonCategory[];
}

async function getReviews(productId: string) {
  return await getRequest(`/api/Review/${productId}`);
}

async function getProduct(id: string) {
  return (await getRequest(`/api/Product/${id}`)) as Product;
}

async function getParameters(id: string) {
  return (await getRequest(`/api/Product/parameters/${id}`)) as ParameterBlock[];
}

async function getProductsOfCategory(
  categoryName: string,
  currentPage: number,
  itemsOnPage: number,
  sortType: string,
  price: number[],
) {
  const sortparams: Sorting = sortTypeParsing(sortType);
  const url = `/api/Category/name/${categoryName}?PageNumber=${currentPage}&ItemsOnPage=${itemsOnPage}&SortingType=${sortparams.type}&ReverseSorting=${sortparams.reverse}&MinPrice=${price[0]}&MaxPrice=${price[1]}`;
  return await getRequest(url);
}

function sortTypeParsing(sortType: string): Sorting {
  if (sortType.includes('reverse')) {
    return { type: sortType.split(' ')[0], reverse: true };
  } else {
    return { type: sortType.split(' ')[0], reverse: false };
  }
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
};
