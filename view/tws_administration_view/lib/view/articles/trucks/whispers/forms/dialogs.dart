part of '../trucks_create_whisper.dart';

// Future<void> _preEvaluationDialog(BuildContext context, bool entrySelected, bool damagedSelected){
//   return showDialog(
//     context: context,
//     builder: (BuildContext context) {
//       return TWSConfirmationDialog(
//         showCancelButton: false,
//         accept: 'Aceptar',
//         title: 'Selecciones faltantes',
//         statement: Text.rich(
//           TextSpan(
//             text: 'Debe seleccionar alguna opción de los siguientes campos: \n\n',
//             children: <InlineSpan>[

//              !entrySelected? const TextSpan(
//                 text: "* Seleccione el tipo de formulario (entrada o salida).\n",
//                 style: TextStyle(fontWeight: FontWeight.w600),
//               ) :const TextSpan(text: ''),

//               !damagedSelected? const TextSpan(
//                 text: "* Seleccione si hubo algun [Tipo de daño].\n",
//                 style: TextStyle(fontWeight: FontWeight.w600),
//               ) :const TextSpan(text: ''),
//             ],
//           ),
//         ),
//         onAccept: () {
//           Navigator.of(context).pop();
//         },
//       );
//     },
//   );
// }

// Future<void> _evaluationDialog(BuildContext context, List<CSMSetValidationResult> errors){
//   return showDialog(
//     context: context,
//     builder: (BuildContext context) {
//       return TWSConfirmationDialog(
//         showCancelButton: false,
//         accept: 'Aceptar',
//         title: 'Errores en el registro',
//         statement: Text.rich(
//           TextSpan(
//             text: 'Verifique los siguientes problemas:\n\n',
//             children: <InlineSpan>[
//               for (int i = 1; i < errors.length + 1; i++)
//                 TextSpan(
//                   text: "$i - ${errors[i - 1].property}: ${errors[i - 1].reason}\n",
//                   style: const TextStyle(fontWeight: FontWeight.w600),
//                 ),
//               const TextSpan(text: ''),
//             ],
//           ),
//         ),
//         onAccept: () {
//           Navigator.of(context).pop();
//         },
//       );
//     },
//   );
// }

Future<void> _exceptionDialog(BuildContext ctx, String title){
  return showDialog(
    context: ctx,
    builder: (BuildContext context) {
      return TWSConfirmationDialog(
        showCancelButton: false,
        accept: 'Aceptar',
        title: 'ha ocurrido error fatal al crear $title',
        statement: const Text.rich(
          textAlign: TextAlign.center,
          TextSpan(
            text: 'Ha ocurrido un error inesperado, por favor intentelo de nuevo. \nEn caso de que el problema persista, contacte al administrador.'
          ),
        ),
        onAccept: () {
          Navigator.of(context).pop();
        },
      );
    },
  );
}

Future<void> _failureDialog(BuildContext ctx, String error, String title, List<Object> failures){
  return showDialog(
    context: ctx,
    builder: (BuildContext context) {
      return TWSConfirmationDialog(
        showCancelButton: false,
        accept: 'Aceptar',
        title: 'ha ocurrido un problema al generar $title',
        statement: Text.rich(
          textAlign: TextAlign.center,
          TextSpan(
            text: "$error \n\n",
            children: <InlineSpan>[
              if(failures.isNotEmpty)
              const TextSpan(
                text: "The following records cannot be created, please verify the data and retry the operation with this specific records: \n\n",
                style: TextStyle(fontWeight: FontWeight.w600),
              ),
              if(failures.isNotEmpty)
              for (int i = 0; i < failures.length; i++)
                TextSpan(
                  text: failures[i] is SetOperationFailure<Truck>? "${i + 1} - Truck with Economic number:  ${(failures[i] as SetOperationFailure<Truck>).set.truckCommonNavigation?.economic}\n"
                  : failures[i] is SetOperationFailure<TruckExternal>? "${i + 1} - External Truck with Economic number:  ${(failures[i] as SetOperationFailure<TruckExternal>).set.truckCommonNavigation?.economic}\n"
                  : "${failures[i].runtimeType} : error on load record data.\n",
                  style: const TextStyle(fontWeight: FontWeight.w600),
                ),
            ],
          )
        ),
        onAccept: () {
          Navigator.of(context).pop();
        },
      );
    },
  );
}

Future<void> _conectionDialog(BuildContext ctx, String title){
  return showDialog(
    context: ctx,
    builder: (BuildContext context) {
      return TWSConfirmationDialog(
        showCancelButton: false,
        accept: 'Aceptar',
        title: 'Problemas de conexión al crear $title',
        statement: const Text.rich(
          textAlign: TextAlign.center,
          TextSpan(
            text: 'No se pudo contactar con el servidor.\n Verifique su conexión a internet o contacte a su administrador.',
          ),
        ),
        onAccept: () {
          Navigator.of(context).pop();
        },
      );
    },
  );
}