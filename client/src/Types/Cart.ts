import CartItem from './CartItem';

type Cart = {
  userId: string;
  products: CartItem[];
};

export default Cart;
