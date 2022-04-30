import FilterValue from 'src/Types/FilterValue';
import ParameterBlock from 'src/Types/ParameterBlock';
import Product from 'src/Types/Product';

interface ISettings {
  currentPage: number;
  sortType: string;
  pickedPrice: number[];
  productList: Product[];
  filters: ParameterBlock[];
  lastPage: number;
  priceRange: number[];
  pickedParams: FilterValue[];

  handlePageChange(event: React.ChangeEvent<unknown>, value: number): void;
  setPage(value: number): void;
  setSortType(type: string): void;
  filtersApplied(): void;
  setParameterValue(newValue: FilterValue): void;
  setInfo(prodList: unknown, maxPage: number, minPrice: number, maxPrice: number, filters: ParameterBlock[]): void;
  checkPickedPrices(i: number, newValue: unknown): void;
  setParams(currPage: unknown, minPrice: unknown, maxPrice: unknown, sort: unknown): void;
  setFilters(params: URLSearchParams): void;
  isItNumber(value: unknown): boolean;
  createURL(): void;
}

function createProdParams(getItems: () => void): ISettings {
  const settings: ISettings = {
    currentPage: 1,
    sortType: 'price',
    pickedPrice: [0, 0],
    productList: [],
    filters: [],
    lastPage: 0,
    priceRange: [0, 0],
    pickedParams: [],

    handlePageChange(event: React.ChangeEvent<unknown>, value: number) {
      this.setPage(value);
    },

    setPage(value: number) {
      if (value <= this.lastPage && value > 0 && value !== 1) {
        this.currentPage = value;
      } else {
        this.currentPage = 1;
      }
      this.createURL();
    },

    setSortType(type: string) {
      if (this.sortType !== type) {
        this.setPage(1);
        this.sortType = type;
        this.createURL();
      }
    },

    filtersApplied() {
      this.setPage(1);
      this.createURL();
    },

    setInfo(prodList: Product[], maxPage: number, minPrice: number, maxPrice: number, filters: ParameterBlock[]) {
      this.productList = prodList;
      this.filters = filters;
      this.lastPage = maxPage;
      this.priceRange = [minPrice, maxPrice];
      if (this.pickedPrice[0] === 0) {
        this.pickedPrice[0] = minPrice;
      }
      if (this.pickedPrice[1] === 0) {
        this.pickedPrice[1] = maxPrice;
      }
    },

    checkPickedPrices(i: number, newValue: unknown) {
      const regexFull = /^\d*$/;
      const regexPunct = /\d+\.\d+/;
      if ((regexFull.test(newValue as string) || regexPunct.test(newValue as string)) && (i === 0 || i === 1)) {
        if ((newValue as string) === '') {
          this.pickedPrice[i] = 0;
        } else {
          this.pickedPrice[i] = newValue as number;
        }
      }
    },

    setParams(currPage: unknown, minPrice: unknown, maxPrice: unknown, sort: unknown) {
      if (this.isItNumber(minPrice)) {
        this.checkPickedPrices(0, minPrice as number);
      }
      if (this.isItNumber(maxPrice)) {
        this.checkPickedPrices(1, maxPrice as number);
      }
      if (sort !== null && sort !== this.sortType) {
        this.sortType = sort as string;
      }
      if (this.isItNumber(currPage)) {
        this.setPage(parseInt(currPage as string));
      }
      this.createURL();
    },

    isItNumber(value: unknown): boolean {
      if (!isNaN(parseInt(value as string))) {
        return true;
      } else {
        return false;
      }
    },

    setParameterValue(newValue: FilterValue) {
      const param = this.pickedParams.find(f => f.id === newValue.id);
      if (param) {
        param.itemIds = newValue.itemIds;
        param.maxValue = newValue.maxValue;
        param.minValue = newValue.minValue;
      } else {
        this.pickedParams.push(newValue);
      }
    },

    setFilters(params: URLSearchParams) {
      this.pickedParams.splice(0, this.pickedParams.length);
      const keys = params.keys();
      let key = keys.next();
      let value;
      while (!key.done) {
        value = params.get(key.value as string)?.split(',');
        if (value && value.length > 0) {
          if (!isNaN(parseInt(value[0] as string))) {
            this.pickedParams.push({ id: key.value as string, range: true, minValue: value[0], maxValue: value[1] });
          } else {
            this.pickedParams.push({ id: key.value as string, range: false, itemIds: value });
          }
        }

        key = keys.next();
      }
    },

    createURL() {
      const params = new URLSearchParams(location.search);
      if (this.currentPage === 1) {
        params.delete('page');
      } else {
        params.set('page', this.currentPage.toString());
      }
      if (this.sortType === 'price') {
        params.delete('sort');
      } else {
        params.set('sort', this.sortType);
      }
      if (this.pickedPrice[0] === this.priceRange[0] && this.pickedPrice[1] === this.priceRange[1]) {
        params.delete('minPrice');
        params.delete('maxPrice');
      } else {
        params.set('minPrice', this.pickedPrice[0].toString());
        params.set('maxPrice', this.pickedPrice[1].toString());
      }

      for (const filter of this.pickedParams) {
        if (filter.range) {
          if (filter.minValue || filter.maxValue) {
            if (!filter.minValue) {
              filter.minValue = '0';
            }
            if (!filter.maxValue) {
              filter.maxValue = '0';
            }
            params.set(filter.id, `${filter.minValue},${filter.maxValue}`);
          }
        } else {
          if (filter.itemIds && filter.itemIds.length > 0) {
            params.set(filter.id, filter.itemIds?.join(','));
          }
        }
      }

      history.pushState({}, 'Products', `?${params.toString()}`);
      getItems();
    },
  };

  return settings;
}

export { createProdParams };
