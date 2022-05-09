import React from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';

import Category from 'src/Types/Category';

interface ICategoryCard {
  category: Category;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    name: {
      fontSize: 1.2 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
  }),
);

const CategoryCard: React.FC<ICategoryCard> = props => {
  const classes = useStyles();
  const redirectionURL = `/category/${props.category.name}`;
  return (
    <Card variant="outlined">
      <Grid item xs={12} container>
        <CardActionArea href={redirectionURL}>
          <Typography className={classes.name} variant="overline">
            {props.category.name}
          </Typography>
        </CardActionArea>
      </Grid>
    </Card>
  );
};

export default CategoryCard;
