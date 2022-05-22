import Category from './Category';
import OutletProduct from './OutletProduct';
import Parameter from './Parameter';

type Product = {
  id: string;
  name: string;
  price: number;
  priceWithoutDiscount?: number;
  description: string;
  mark: number;
  vendorCode: string;
  picURL: string;
  categoryIdFk: string;
  category: Category | null;
  inCart: boolean;
  inWishlist: boolean;
  published: boolean;
  reviewed: boolean;
  isInWarehouse: boolean;
  outletProducts: OutletProduct[];
};

export default Product;
