import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { useParams } from 'react-router-dom';
import { Card, Typography } from '@material-ui/core';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';

import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getProduct } from 'src/Requests/GetRequests';
import Product from 'src/Types/Product';
import DetailedProductCard from 'src/Components/Parts/DetailedProductCard';
import ProductInfoPanel from 'src/Components/Parts/ProductInfoPanel';

interface IProductPage {
  productID: string;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    prodName: {
      fontSize: 2 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
  }),
);

const ProductPage: React.FC = () => {
  const params: IProductPage = useParams();
  const classes = useStyles();

  const [product, setProduct] = useState<Product>();
  const [picture, setPicture] = useState<string>('');
  const picUrl = 'https://localhost:5001/products/';

  useEffect(() => {
    let isMounted = true;
    const getProd = async () => {
      const res = await getProduct(params.productID);
      if (isMounted) {
        setProduct(res);

        const img = new Image();
        img.src = `${picUrl}${res.picURL}`;

        if (isMounted) {
          img.onload = () => setPicture(`${picUrl}${res.picURL}`);
          img.onerror = () => setPicture(`${picUrl}noPic.jpg`);
        }
      }
    };
    getProd();

    return () => {
      isMounted = false;
    };
  }, [params.productID]);

  return (
    <React.Fragment>
      <NavigationBar />
      <Grid container direction="row" justify="center" alignItems="center">
        <Grid xs={12} sm={7} item direction="column" justify="center" alignItems="center" container>
          <Card>
            <Grid>
              <Typography className={classes.prodName} variant="overline" component="h4">
                {product?.name}
              </Typography>
            </Grid>
            {product && <DetailedProductCard product={product} image={picture} />}
            {product && <ProductInfoPanel product={product} />}
          </Card>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default ProductPage;