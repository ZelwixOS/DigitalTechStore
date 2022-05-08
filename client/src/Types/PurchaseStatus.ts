const PurchaseStatus = {
  waitsForPaymentAprroval: 0,
  waitsInOulet: 1,
  transportingToOutlet: 2,
  waitsForDelivery: 3,
  inDelivering: 4,
  refused: 5,
  canceledByClient: 6,
  finished: 7,
};

const getStatusString = (id: number) => {
  switch (id) {
    case PurchaseStatus.waitsForPaymentAprroval:
      return 'Ожидается подтверждения оплаты';
    case PurchaseStatus.waitsInOulet:
      return 'Ожидает в магазине';
    case PurchaseStatus.transportingToOutlet:
      return 'Доставляется в магазин';
    case PurchaseStatus.waitsForDelivery:
      return 'Ожидается доставка';
    case PurchaseStatus.inDelivering:
      return 'Доставляется';
    case PurchaseStatus.refused:
      return 'Отказ';
    case PurchaseStatus.canceledByClient:
      return 'Закрыт';
    case PurchaseStatus.finished:
      return 'Завершён';
    default:
      return 'Временно недоступно';
  }
};

export { PurchaseStatus, getStatusString };
