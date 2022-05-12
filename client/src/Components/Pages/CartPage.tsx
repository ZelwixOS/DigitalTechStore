import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';

import ProductCard from 'src/Components/Parts/ProductCard';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getCart, getCartUnsigned } from 'src/Requests/GetRequests';
import CartItem from 'src/Types/CartItem';
import Cart from 'src/Types/Cart';
import { deleteFromCart } from 'src/Requests/DeleteRequests';
import { updateCartItem } from 'src/Requests/PutRequests';
import Product from 'src/Types/Product';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    productGrid: {
      paddingLeft: theme.spacing(1),
      paddingTop: theme.spacing(1),
      paddingBottom: theme.spacing(2),
    },
    filterPanel: {
      paddingTop: theme.spacing(1),
    },
    pageName: {
      paddingTop: theme.spacing(2),
      fontWeight: theme.typography.fontWeightBold,
    },
  }),
);

const CartPage: React.FC = () => {
  const getProd = async (isMounted: boolean) => {
    let res: Cart;
    const role = sessionStorage.getItem('signed');
    if (role || role === 'Guest') {
      res = (await getCart()) as Cart;
    } else {
      res = await getCartUnsigned();
    }

    if (isMounted) {
      setCartItems(res.products);
    }
  };

  const updateCurrentSumm = (pickedProducts: Product[], cartItems: CartItem[]) => {
    let summ = 0;
    let prod;
    for (const element of pickedProducts) {
      prod = cartItems.find(item => item.product.id === element.id);
      if (prod) {
        summ += prod.count * prod.product.price;
      }
    }

    setCurrentSumm(summ);
    return summ;
  };

  const onCount = (newCount: number, id?: string) => {
    const role = sessionStorage.getItem('signed');
    if (!role) {
      updateCartItem(newCount, id as string);
    }

    const itemId = cartItems.findIndex(item => item.product.id === (id as string));
    cartItems[itemId].count = newCount;
    updateCurrentSumm(pickedProducts, cartItems);
  };

  const deleteItem = async (productId: string) => {
    const role = sessionStorage.getItem('signed');
    console.log(role);
    let result;
    if (role && role !== 'Guest') {
      result = await deleteFromCart(productId);
    } else {
      const cart = localStorage.getItem('cartItems');
      if (cart) {
        const arr = cart.split(',');
        const index = arr.findIndex(a => a === productId);
        arr.splice(index, 1);
        localStorage.setItem('cartItems', arr.join(','));
      }
    }

    if (!role || result === 1) {
      const deleted = cartItems.findIndex(item => item.product.id === productId);
      const newCartItems = [...cartItems];
      newCartItems.splice(deleted, 1);

      const deletedPicked = pickedProducts.findIndex(item => item.id === productId);
      const picked = [...pickedProducts];
      picked.splice(deletedPicked, 1);
      setPickedProducts(picked);

      setCartItems(newCartItems);
      updateCurrentSumm(pickedProducts, newCartItems);
    }
  };

  const onChecked = (product: Product, added: boolean) => {
    let newPicked;
    if (added) {
      newPicked = [...pickedProducts, product];
      setPickedProducts(newPicked);
    } else {
      const deleted = pickedProducts.findIndex(item => item.id === product.id);
      newPicked = [...pickedProducts];
      newPicked.splice(deleted, 1);
      setPickedProducts(newPicked);
    }

    updateCurrentSumm(newPicked, cartItems);
  };

  const onPuchaseClick = () => {
    if (pickedProducts.length > 0) {
      let picked = '';
      for (const item of pickedProducts) {
        picked += `${item.id}_${cartItems.find(p => p.product.id === item.id)?.count},`;
      }
      picked = picked.substring(0, picked.length - 1);

      history.pushState({}, 'DTS', `/purchasing?items=${picked}`);
      window.location.replace(`/purchasing?items=${picked}`);
    }
  };

  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [pickedProducts, setPickedProducts] = useState<Product[]>([]);
  const [currentSumm, setCurrentSumm] = useState<number>(0);

  useEffect(() => {
    let isMounted = true;
    getProd(isMounted);
    return () => {
      isMounted = false;
    };
  }, []);

  const classes = useStyles();

  return (
    <React.Fragment>
      <NavigationBar />
      <Grid container direction="row" justify="center" alignItems="center">
        <Grid xs={12} sm={9} item direction="row" justify="center" alignItems="center" container>
          <Grid item direction="column" justify="center" container>
            <Grid>
              <Typography align="center" className={classes.pageName} variant="h5" component="h5">
                Корзина
              </Typography>
            </Grid>
            <Grid item direction="row" justify="center" container>
              <Grid className={classes.productGrid} xs={12} sm={10} item container direction="column">
                <Grid>
                  {cartItems &&
                    cartItems.map(cartItem => (
                      <ProductCard
                        product={cartItem.product}
                        key={cartItem.product.id}
                        hideBuy={true}
                        hideLike={true}
                        showCounter={true}
                        count={cartItem.count}
                        onCount={onCount}
                        onDelete={deleteItem}
                        onChecked={onChecked}
                      />
                    ))}
                </Grid>
              </Grid>
            </Grid>
            <Grid container direction="row" justify="space-evenly" alignItems="center">
              <Typography className={classes.pageName} variant="h6" component="h6">
                Сумма: {currentSumm} ₽
              </Typography>
              <Button variant="contained" disabled={pickedProducts.length < 1} onClick={onPuchaseClick} color="primary">
                Оформить заказ
              </Button>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default CartPage;
