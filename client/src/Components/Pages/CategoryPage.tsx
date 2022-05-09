import React, { useEffect } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import { Typography } from '@material-ui/core';
import { useParams } from 'react-router-dom';
import Pagination from '@material-ui/lab/Pagination';
import { Observer, useLocalObservable } from 'mobx-react';

import ProductCard from 'src/Components/Parts/ProductCard';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getProductsOfCategory } from 'src/Requests/GetRequests';
import SortBar from 'src/Components/Parts/SortBar';
import FilterBar from 'src/Components/Parts/FilterBar';
import { createProdParams } from 'src/Components/Parts/ProductListParams';

interface ICategoryPage {
  categoryName: string;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    productGrid: {
      paddingLeft: theme.spacing(1),
      paddingTop: theme.spacing(1),
      paddingBottom: theme.spacing(2),
    },
    categoryWord: {
      fontWeight: 600,
      padding: theme.spacing(2),
    },
    filterPanel: {
      paddingTop: theme.spacing(1),
    },
  }),
);

const CategoryPage: React.FC = () => {
  const params = useParams();

  const object = createProdParams(async () => {
    const res = await getProductsOfCategory(
      params?.categoryName as string,
      data.currentPage,
      10,
      data.sortType,
      data.pickedPrice,
      data.pickedParams,
    );

    data.setInfo(res.container.products, res.maxPage, res.minPrice, res.maxPrice, res.container.parameterBlocks);
  });
  const data = useLocalObservable(() => object);

  const getProducts = async (isMounted: boolean) => {
    const qParams = new URLSearchParams(location.search);
    data.setParams(qParams.get('page'), qParams.get('minPrice'), qParams.get('maxPrice'), qParams.get('sort'));
    data.setFilters(qParams);

    const res = await getProductsOfCategory(
      params?.categoryName as string,
      data.currentPage,
      10,
      data.sortType,
      data.pickedPrice,
      data.pickedParams,
    );
    if (isMounted !== false && res.container !== undefined) {
      data.setInfo(res.container.products, res.maxPage, res.minPrice, res.maxPrice, res.container.parameterBlocks);
    }
  };

  useEffect(() => {
    let isMounted = true;
    getProducts(isMounted);
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
          <Grid item direction="column" justify="center" alignItems="center" container>
            <Grid>
              <Typography className={classes.categoryWord} variant="h5" component="h5">
                {params.categoryName}
              </Typography>
            </Grid>
            <Grid container justify="center" alignItems="center">
              <Observer>{() => <SortBar type={data.sortType} onChange={data.setSortType} />}</Observer>
            </Grid>
            <Grid item direction="row" justify="center" container>
              <Observer>
                {() => (
                  <Grid xs={12} sm={3} item className={classes.filterPanel}>
                    <FilterBar
                      priceRange={data.priceRange}
                      pickedPrices={data.pickedPrice}
                      parameterBlocks={data.filters}
                      pickedParams={data.pickedParams}
                      setPrices={data.checkPickedPrices}
                      applyChanges={data.filtersApplied}
                      setParameter={data.setParameterValue}
                    />
                  </Grid>
                )}
              </Observer>
              <Grid className={classes.productGrid} xs={12} sm={9} item container direction="column">
                <Observer>
                  {() => (
                    <Grid>
                      {data.productList.map(product => (
                        <ProductCard product={product} key={product.id} />
                      ))}
                    </Grid>
                  )}
                </Observer>
                <Grid justify="center" alignItems="center" container>
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

export default CategoryPage;
