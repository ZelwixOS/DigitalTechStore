import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

import ProductCard from 'src/Components/Parts/ProductCard';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getCart } from 'src/Requests/GetRequests';
import CartItem from 'src/Types/CartItem';
import Cart from 'src/Types/Cart';
import { deleteFromCart } from 'src/Requests/DeleteRequests';
import { updateCartItem } from 'src/Requests/PutRequests';

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
    const res = (await getCart()) as Cart;
    if (isMounted) {
      setCartItems(res.products);
    }
  };

  const onCount = (newCount: number, id?: string) => {
    updateCartItem(newCount, id as string);
  };

  const deleteItem = async (productId: string) => {
    const result = await deleteFromCart(productId);
    if (result === 1) {
      const deleted = cartItems.findIndex(item => item.product.id === productId);
      const newCartItems = cartItems.slice().splice(deleted, 1);
      setCartItems(newCartItems);
    }
  };

  const [cartItems, setCartItems] = useState<CartItem[]>([]);

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
              <Typography className={classes.pageName} variant="h5" component="h5">
                Корзина
              </Typography>
            </Grid>
            <Grid item direction="row" justify="center" container>
              <Grid className={classes.productGrid} xs={12} sm={9} item container direction="column">
                <Grid>
                  {cartItems.map(cartItem => (
                    <ProductCard
                      product={cartItem.product}
                      key={cartItem.product.id}
                      hideBuy={true}
                      hideLike={true}
                      showCounter={true}
                      count={cartItem.count}
                      onCount={onCount}
                      onDelete={deleteItem}
                    />
                  ))}
                </Grid>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default CartPage;
