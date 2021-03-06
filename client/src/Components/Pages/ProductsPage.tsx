import React, { useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import Pagination from '@material-ui/lab/Pagination';
import { Observer, useLocalObservable } from 'mobx-react';

import ProductCard from 'src/Components/Parts/ProductCard';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getProducts } from 'src/Requests/GetRequests';
import SortBar from 'src/Components/Parts/SortBar';
import { createProdParams } from 'src/Components/Parts/ProductListParams';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    productGrid: {
      paddingTop: theme.spacing(1),
      paddingBottom: theme.spacing(2),
    },
    filterPanel: {
      paddingTop: theme.spacing(1),
    },
  }),
);

const ProductsPage: React.FC = () => {
  const getProd = async (isMounted: boolean) => {
    const res = await getProducts(data.currentPage, 10, data.sortType, data.pickedPrice);
    if (isMounted) {
      const params = new URLSearchParams(location.search);
      data.setInfo(res.container, res.maxPage, res.minPrice, res.maxPrice, []);
      data.setParams(params.get('page'), params.get('minPrice'), params.get('maxPrice'), params.get('sort'));
    }
  };

  const object = createProdParams(async () => {
    const res = await getProducts(data.currentPage, 9, data.sortType, data.pickedPrice);
    data.setInfo(res.container, res.maxPage, res.minPrice, res.maxPrice, []);
  });

  const data = useLocalObservable(() => object);

  useEffect(() => {
    let isMounted = true;
    getProd(isMounted);
    return () => {
      isMounted = false;
    };
  });

  const classes = useStyles();

  return (
    <React.Fragment>
      <NavigationBar />
      <Grid container direction="row" justifyContent="center" alignItems="center">
        <Grid xs={12} sm={9} item direction="row" justifyContent="center" alignItems="center" container>
          <Grid item direction="column" justifyContent="center" container>
            <Grid container justifyContent="center" alignItems="center">
              <Observer>{() => <SortBar type={data.sortType} onChange={data.setSortType} />}</Observer>
            </Grid>
            <Grid item direction="row" justifyContent="center" container>
              <Grid className={classes.productGrid} xs={12} sm={12} item container direction="column">
                <Observer>
                  {() => (
                    <Grid>
                      {data.productList.map(product => (
                        <ProductCard product={product} key={product.id} />
                      ))}
                    </Grid>
                  )}
                </Observer>
                <Grid justifyContent="center" alignItems="center" container>
                  <Observer>
                    {() => (
                      <React.Fragment>
                        {data.lastPage > 1 && (
                          <Pagination
                            count={data.lastPage}
                            page={data.currentPage}
                            onChange={data.handlePageChange}
                            variant="outlined"
                            color="primary"
                            shape="rounded"
                          />
                        )}
                      </React.Fragment>
                    )}
                  </Observer>
                </Grid>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default ProductsPage;
