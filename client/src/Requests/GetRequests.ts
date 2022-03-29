import axios from 'axios';

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

async function getCategories() {
  return await getRequest('/api/Category');
}

async function getProduct(id: string) {
  return await getRequest(`/api/Product/${id}`);
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

export { getProducts, getRequest, getCategories, getProductsOfCategory, getProduct, getCart, getWishlist };
