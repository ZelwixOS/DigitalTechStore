import Category from './Category';

type CommonCategory = {
  id: string;
  name: string;
  description: string;
  categories: Category[];
};

export default CommonCategory;
