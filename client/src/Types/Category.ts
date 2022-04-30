import ParameterBlock from './ParameterBlock';
import Product from './Product';

type Category = {
  id: string;
  name: string;
  products: Product[];
  parameterBlocks: ParameterBlock[];
};

export default Category;
