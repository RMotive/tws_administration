import 'package:csm_foundation_services/csm_foundation_services.dart';

/// Represents the success frame for responses from [TWSAdministration] source
/// exposing static properties from transaction contexts.
///
/// [TEstela] : Model object type that represents the transaction context result.
final class SuccessFrame<TEstela extends CSMEncodeInterface> implements CSMEncodeInterface {
  /// Unique transaction identifier.
  final String tracer;

  /// Transaction context object.
  final TEstela estela;

  /// Generates a new success frame object representation.
  const SuccessFrame(this.tracer, this.estela);

  /// Generates a new success frame object decoding a json object.
  factory SuccessFrame.des(
    JObject json, {
    CSMDecodeInterface<TEstela>? estelaDecoder,
  }) {
    String tracer = json.get('tracer');

    JObject objEstela = json.getDefault('estela', <String, dynamic>{});
    TEstela estelaObject = deserealize(objEstela, decode: estelaDecoder);

    return SuccessFrame<TEstela>(tracer, estelaObject);
  }

  @override
  JObject encode() {
    return <String, dynamic>{
      'tracer': '',
      'estela': estela.encode(),
    };
  }
}

final class SuccessFrameDecode<TEstela extends CSMEncodeInterface> implements CSMDecodeInterface<SuccessFrame<TEstela>> {
  final CSMDecodeInterface<TEstela> estelaDecoder;

  const SuccessFrameDecode(this.estelaDecoder);

  @override
  SuccessFrame<TEstela> decode(JObject json) {
    return SuccessFrame<TEstela>.des(
      json,
      estelaDecoder: estelaDecoder,
    );
  }
}
