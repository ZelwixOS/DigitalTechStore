import React from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import { Typography } from '@material-ui/core';
import { useParams } from 'react-router-dom';

import CategoryCard from 'src/Components/Parts/CategoryCard';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getCategories } from 'src/Requests/GetRequests';
import Category from 'src/Types/Category';

interface ICommonCategoryPage {
  commonCategoryName: string;
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

const CommonCategoryPage: React.FC = () => {
  const params: ICommonCategoryPage = useParams();

  const [categories, setCategories] = React.useState<Category[]>([]);

  const createRow = (index: number, array: Category[]) => (
    <Grid container item xs={12} alignItems="stretch" justify="space-evenly" spacing={2}>
      <Grid item xs={4}>
        {array[index] && <CategoryCard category={array[index]} />}
      </Grid>
      <Grid item xs={4}>
        {array[index + 1] && <CategoryCard category={array[index + 1]} />}
      </Grid>
      <Grid item xs={4}>
        {array[index + 2] && <CategoryCard category={array[index + 2]} />}
      </Grid>
    </Grid>
  );

  React.useEffect(() => {
    let isMounted = true;
    const getCategs = async () => {
      const res = await getCategories(params.commonCategoryName);
      if (isMounted) {
        setCategories(res);
      }
    };
    getCategs();

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
                {params.commonCategoryName}
              </Typography>
              <Grid container spacing={1}>
                {categories.map((item, index, arr) => index % 3 === 0 && createRow(index, arr))}
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default CommonCategoryPage;