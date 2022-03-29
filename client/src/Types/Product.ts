import Category from './Category';
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
  productParameter: Parameter[];
  inCart: boolean;
  inWishlist: boolean;
};

export default Product;
