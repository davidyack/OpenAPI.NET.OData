﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.OData
{
    /// <summary>
    /// Extension methods to convert <see cref="IEdmModel"/> to <see cref="OpenApiDocument"/>.
    /// </summary>
    public static class EdmModelOpenApiMappingExtensions
    {
        /// <summary>
        /// Convert <see cref="IEdmModel"/> to <see cref="OpenApiDocument"/> using a configure action.
        /// </summary>
        /// <param name="model">The Edm model.</param>
        /// <param name="configure">The configure action.</param>
        /// <returns>The converted Open API document object.</returns>
        public static OpenApiDocument ConvertToOpenApi(this IEdmModel model, Action<OpenApiDocument> configure)
        {
            if (model == null)
            {
                throw Error.ArgumentNull(nameof(model));
            }

            OpenApiDocument document = model.CreateDocument();
            configure?.Invoke(document);
            return document;
        }

        /// <summary>
        /// Convert <see cref="IEdmModel"/> to <see cref="OpenApiDocument"/>.
        /// </summary>
        /// <param name="model">The Edm model.</param>
        /// <returns>The converted Open API document object.</returns>
        public static OpenApiDocument ConvertToOpenApi(this IEdmModel model)
        {
            if (model == null)
            {
                throw Error.ArgumentNull(nameof(model));
            }

            return model.CreateDocument();
        }
    }
}