import React from 'react';
import Card from '@material-ui/core/Card';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Rating from '@material-ui/lab/Rating';
import { Button } from '@material-ui/core';
import FavoriteIcon from '@material-ui/icons/Favorite';
import IconButton from '@material-ui/core/IconButton';
import Grid from '@material-ui/core/Grid';

interface IPriceLikeBuyCard {
  price?: number;
  id?: string;
  rating?: number;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      minHeight: 180,
      margin: theme.spacing(1),
    },
    grid: {
      minHeight: 80,
    },
    details: {
      display: 'flex',
      flexDirection: 'column',
    },
    content: {
      flex: '1 0 auto',
    },
    cover: {
      maxWidth: 151,
      maxHeight: 120,
      margin: theme.spacing(2),
    },
    bold: {
      fontWeight: 600,
    },
    rating: {
      margin: theme.spacing(2),
    },
  }),
);

const PriceLikeBuyCard: React.FC<IPriceLikeBuyCard> = props => {
  const classes = useStyles();

  return (
    <Card variant="outlined" className={classes.root}>
      <Grid justify="center" alignItems="center" container direction="column">
        <Grid className={classes.grid} container direction="row" justify="center" alignItems="center">
          <Grid item container direction="column" justify="center" alignItems="center" xs={12} sm={6}>
            <Typography component="h5" variant="h5" className={classes.bold}>
              Price:
            </Typography>
            <Typography color="primary" component="h5" variant="h5" className={classes.bold}>
              ${props.price}
            </Typography>
          </Grid>
          <Grid item xs={12} sm={6} container direction="row" justify="center" alignItems="center">
            <Grid item xs={12} sm={3} container justify="flex-start">
              <IconButton aria-label="favourite">
                <FavoriteIcon />
              </IconButton>
            </Grid>
            <Grid item xs={12} sm={9}>
              <Button color="primary" variant="contained">
                Buy
              </Button>
            </Grid>
          </Grid>
        </Grid>
        <Rating className={classes.rating} name="read-only" value={props.rating} readOnly />
      </Grid>
    </Card>
  );
};

export default PriceLikeBuyCard;
