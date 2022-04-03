import React, { useEffect, useState } from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import ReviewBlock from 'src/Components/Parts/ReviewBlock';
import Review from 'src/Types/Review';
import ReviewForm from 'src/Components/Parts/ReviewForm';
import { getReviews } from 'src/Requests/GetRequests';

interface IProductReviews {
  productName: string;
  productId: string;
  reviewed: boolean;
  saveReviewed: () => void;
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

  const getProductReviews = async (isMounted: boolean) => {
    const res = await getReviews(props.productId);
    if (isMounted !== false) {
      setReviews(res as Review[]);
    }
  };

  useEffect(() => {
    let isMounted = true;
    getProductReviews(isMounted);
    return () => {
      isMounted = false;
    };
  }, []);

  const [reviewed, setReviewed] = useState<boolean>(props.reviewed);
  const [reviews, setReviews] = useState<Review[]>([]);

  const addReview = (newReview: Review): void => {
    const newVar: Review[] = [];
    while (reviews.length > 0) {
      newVar.push(reviews.pop() as Review);
    }
    newVar.push(newReview);
    setReviews(newVar.reverse());

    setReviewed(true);
    props.saveReviewed();
  };

  return (
    <Grid container direction="column" alignItems="center" justify="center">
      <Typography className={classes.prodName} variant="overline">
        {`Отзывы на ${props.productName}`}
      </Typography>
      <Grid item xs={12} direction="column" container justify="center">
        {!reviewed && <ReviewForm addReview={addReview} productId={props.productId} />}
        {reviews?.map((review, index) => (
          <ReviewBlock review={review} key={index} />
        ))}
      </Grid>
    </Grid>
  );
};

export default ProductReviews;
