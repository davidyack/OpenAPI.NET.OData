﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Validation;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.OData.Common;
using Microsoft.OpenApi.OData.Edm;
using Microsoft.OpenApi.OData.Generator;

namespace Microsoft.OpenApi.OData
{
    /// <summary>
    /// Extension methods to convert <see cref="IEdmModel"/> to <see cref="OpenApiDocument"/>.
    /// </summary>
    public static class EdmModelOpenApiExtensions
    {
        /// <summary>
        /// Convert <see cref="IEdmModel"/> to <see cref="OpenApiDocument"/>.
        /// </summary>
        /// <param name="model">The Edm model.</param>
        /// <returns>The converted Open API document object, <see cref="OpenApiDocument"/>.</returns>
        public static OpenApiDocument ConvertToOpenApi(this IEdmModel model)
        {
            return model.ConvertToOpenApi(new OpenApiConvertSettings());
        }

        /// <summary>
        /// Convert <see cref="IEdmModel"/> to <see cref="OpenApiDocument"/> using a convert settings.
        /// </summary>
        /// <param name="model">The Edm model.</param>
        /// <param name="settings">The convert settings.</param>
        /// <returns>The converted Open API document object, <see cref="OpenApiDocument"/>.</returns>
        public static OpenApiDocument ConvertToOpenApi(this IEdmModel model, OpenApiConvertSettings settings)
        {
            Utils.CheckArgumentNull(model, nameof(model));
            Utils.CheckArgumentNull(settings, nameof(settings));

            if (settings.VerifyEdmModel)
            {
                IEnumerable<EdmError> errors;
                if (!model.Validate(out errors))
                {
                    OpenApiDocument document = new OpenApiDocument();
                    int index = 1;
                    foreach (var error in errors)
                    {
                        document.Extensions.Add(Constants.xMsEdmModelError + index++, new OpenApiString(error.ToString()));
                    }

                    return document;
                }
            }

            ODataContext context = new ODataContext(model, settings);
            return context.CreateDocument();
        }
    }
}
