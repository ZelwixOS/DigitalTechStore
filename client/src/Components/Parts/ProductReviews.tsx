import React, { useState } from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import ReviewBlock from 'src/Components/Parts/ReviewBlock';
import Review from 'src/Types/Review';
import ReviewForm from 'src/Components/Parts/ReviewForm';

interface IProductReviews {
  productName: string;
  reviews: Review[];
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    prodName: {
      fontSize: 1.3 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
  }),
);

const ProductReviews: React.FC<IProductReviews> = props => {
  const classes = useStyles();
  const [reviews, setRerviews] = useState<Review[]>(props.reviews);
  const addReview = (newReview: Review): void => {
    const newVar: Review[] = [];
    while (reviews.length > 0) {
      newVar.push(reviews.pop() as Review);
    }
    newVar.push(newReview);
    setRerviews(newVar.reverse());
  };
  return (
    <Grid container direction="column" alignItems="center" justify="center">
      <Typography className={classes.prodName} variant="overline">
        {`Reviews on ${props.productName}`}
      </Typography>
      <Grid item xs={12} direction="column" container justify="center">
        <ReviewForm addReview={addReview} />
        {reviews?.map((review, index) => (
          <ReviewBlock review={review} key={index} />
        ))}
      </Grid>
    </Grid>
  );
};

export default ProductReviews;
