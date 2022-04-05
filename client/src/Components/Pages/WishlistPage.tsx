import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

import ProductCard from 'src/Components/Parts/ProductCard';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getWishlist } from 'src/Requests/GetRequests';
import { deleteFromWishlist } from 'src/Requests/DeleteRequests';
import Product from 'src/Types/Product';
import Wishlist from 'src/Types/Wishlist';

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

const WishlistPage: React.FC = () => {
  const getProd = async (isMounted: boolean) => {
    const res = (await getWishlist()) as Wishlist;
    if (isMounted) {
      setWishlistItems(res.products);
    }
  };

  const deleteItem = async (productId: string) => {
    const result = await deleteFromWishlist(productId);
    if (result === 1) {
      const deleted = wishlistItems.findIndex(item => item.id === productId);
      const newWishlistItems = [...wishlistItems];
      newWishlistItems.splice(deleted, 1);
      console.log(newWishlistItems);
      setWishlistItems(newWishlistItems);
    }
  };

  const [wishlistItems, setWishlistItems] = useState<Product[]>([]);

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
                Избранное
              </Typography>
            </Grid>
            <Grid item direction="row" justify="center" container>
              <Grid className={classes.productGrid} xs={12} sm={9} item container direction="column">
                <Grid>
                  {wishlistItems.map(wishlistItem => (
                    <ProductCard
                      product={wishlistItem}
                      key={wishlistItem.id}
                      hideBuy={true}
                      hideLike={true}
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

export default WishlistPage;
