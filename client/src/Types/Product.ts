import Category from './Category';
import OutletProduct from './OutletProduct';
import Parameter from './Parameter';

type Product = {
  id: string;
  name: string;
  price: number;
  description: string;
  mark: number;
  vendorCode: string;
  picURL: string;
  categoryIdFk: string;
  category: Category | null;
  inCart: boolean;
  inWishlist: boolean;
  reviewed: boolean;
  isInWarehouse: boolean;
  outletProducts: OutletProduct[];
};

export default Product;
