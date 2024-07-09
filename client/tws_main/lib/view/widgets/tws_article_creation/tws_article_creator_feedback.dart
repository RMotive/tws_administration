enum TWSArticleCreatorFeedbackTypes {
  error,
  success,
  message,
  warning,
}

final class TWSArticleCreatorFeedback {
  final TWSArticleCreatorFeedbackTypes type;

  const TWSArticleCreatorFeedback(this.type);
}
